# Latency Sensitive Spec

This document specifies the initial on-wire format for Smoosh™.

## `unit`

Unit is represented with zero bits.


## `bool`

Encoded as a single bit.


## `byte`

Bytes are encoded as-is.


## Zig Zag Encoding, and `int8`, `int16`, `int32`, `int64`

Signed integers are stored using the [Zig-Zag Encoding](https://en.wikipedia.org/wiki/Variable-length_quantity#Zigzag_encoding).

Conceptually: A positive integer `n` is encoded as `n*2`, and a negative value is encoded as `abs(n*2) - 1`.

This is equivalent to: `((n << 1) ^ (n >> (numBits(n) - 1))`, and this equivalent function is significantly faster.

Decoding is then implemented as: `((n >> 1) ^ -(n & 1))`


## `uint16`, `uint32`, `uint64`, and the Latency Smoosh™ encoding

Values of the type `uint16`, `uint32`, and `uint64` are encoded as-is, however there are a few cases where values of `uint16`
are specially encoded, I call this the Smoosh encoding.

`uint16`s, when Smoosh encoded have 1 bit headers defining if the value is less than `0xff`.

If the value is less than `0xff`, then it is stored in 8 bits, otherwise the header and full 16 bit payload is encoded.

For example:

```
++---++-------------------------------++
|| H || P | P | P | P | P | P | P | P ||
++---++-------------------------------++
|| 0 || b | b | b | b | b | b | b | b ||
++---++-------------------------------++

++---++---------------------------------------------------------------++
|| H || P | P | P | P | P | P | P | P | P | P | P | P | P | P | P | P ||
++---++---------------------------------------------------------------++
|| 1 || b | b | b | b | b | b | b | b | b | b | b | b | b | b | b | b ||
++---++---------------------------------------------------------------++
```

This is commonly used for encoding the length of some collection, since it will be likely that the collection is small in latency
sensitive applications, and it enables the ability to send a few large collections of rather small types over the 256 limit, 
while only costing 1 bit per collection encoded.


## `single`, `double`, `decimal`

Singles and doubles are encoded as is.

Decimals are to their 128 bit representation, and encoded as is.

## `char`

`char`s in .Net are UTF-16 characters, and are encoded as-is.

Support for UTF-8 to be determined.


## `string`

Length is encoded with a Smooshed `uint16`, then directly encoded as UTF-16.

Support for UTF-8 to be determined.


# `DateTime`, `TimeSpan`, and `DateTimeOffset`

Noting the [Limitations](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.ticks?view=net-7.0#remarks) of `Ticks` in
.Net, Smoosh makes no attempt to improve the situation.
If you are building applications that require time resolutions finer than 100ns, then consider not using .Net.

With that note: `DateTime` and `TimeSpan` have their `Ticks` Zig-Zag encoded.

`DateTimeOffset` has their `Ticks` Zig-Zag encoded, and then it's `Offset.Ticks` is immediately Zig-Zag encoded.


# `Guid`

Encoded as-is.


# `BigInt`

Length is encoded with a Smooshed `uint16`, then directly encoded as-is.


## `array`

An `array` has a Smooshed `uint16` header, defining the number of elements the array contains, and it's members are immediately encoded.


## `list`

A `list` in F# is defined as a discriminated union, and has identical encoding.


## `map`

An `map` has Smooshed `uint16` header, defining the number of elements the array contains, and then it's members are immediately encoded.


## Records

Records have their members immediately encoded in the order of their definition.


## Discriminated unions

Discriminated unions are encoded by a variable length header.

The length of the header is defined by the number of bits required to encode the number of instances of that type, given as: `floor (log2 n) + 1`

Each instance's header is encoded by a bit sequence, truncated to this length: Expressing the index of the instance of the union.

If the instance contains additional values, they are then immediately encoded.


# Padding

After encoding, the bit-level result is padded at the end with `0`s to some multiple of 8, for byte alignment.