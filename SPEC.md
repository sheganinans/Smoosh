# General Spec

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


## `uint16`, `uint32`, `uint64`, and the General Smoosh™ encoding

Values of the type `uint16`, `uint32`, and `uint64`, without the  `[<Smoosh>]` attribute are encoded as-is.

### `[<Smoosh>]` encoded integers:

For example, `uint16`s, when Smoosh encoded will always end with a `0` bit, determining the end of the Smooshed integer.

Otherwise each byte will be prepended with a `1` bit, denoting an additional byte.

### For example:

```
++---++-------------------------------++---++
|| H || P | P | P | P | P | P | P | P || T ||
++---++-------------------------------++---++
|| 1 || b | b | b | b | b | b | b | b || 0 ||
++---++-------------------------------++---++

++---++-------------------------------++---++-------------------------------++---++
|| H || P | P | P | P | P | P | P | P || H || P | P | P | P | P | P | P | P || T ||
++---++-------------------------------++---++-------------------------------++---++
|| 1 || b | b | b | b | b | b | b | b || 1 || b | b | b | b | b | b | b | b || 0 ||
++---++-------------------------------++---++-------------------------------++---++
```

## `single`, `double`, `decimal`

Singles and doubles are encoded as is.

Decimals are to their 128 bit representation, and encoded as is.

## `char`

`char`s in .Net are UTF-16 characters, and are encoded as-is.

Support for UTF-8 to be determined.


## `string`

Without the `[<Utf8>]` attribute: Length is encoded with a Smooshed `uint32`, then directly encoded as-is.

### `[<Utf8>]` encoded strings

Before encoding, strings will be converted to their UTF8 representation, length is encoded as a Smooshed `uint32`,
and then encoded as-is.


# `DateTime`, `TimeSpan`, and `DateTimeOffset`

Noting the [Limitations](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.ticks?view=net-7.0#remarks) of `Ticks` in
.Net, Smoosh makes no attempt to improve the situation.
If you are building applications that require time resolutions finer than 100ns, then consider not using .Net.

With that note: `DateTime` and `TimeSpan` have their `Ticks` Zig-Zag encoded.

`DateTimeOffset` has their `Ticks` Zig-Zag encoded, and then it's `Offset.Ticks` is immediately Zig-Zag encoded.


# `Guid`

Encoded as-is.


# `BigInt`

Length is encoded with a Smooshed `uint32`, then directly encoded as-is.


## `array`

An `array` has a Smooshed `uint32` header, defining the number of elements the array contains, and it's members are immediately encoded.


## `list`

A `list` in F# is defined as a discriminated union, and has identical encoding.


## `map`

An `map` has Smooshed `uint32` header, defining the number of elements the array contains, and then it's members are immediately encoded.


## Records

Records have their members immediately encoded in the order of their definition.


## Discriminated unions

Discriminated unions are encoded by a variable length header.

The length of the header is defined by the number of bits required to encode the number of instances of that type, given as: `floor (log2 n) + 1`

Each instance's header is encoded by a bit sequence, truncated to this length: Expressing the index of the instance of the union.

If the instance contains additional values, they are then immediately encoded.


# Padding

After encoding, the bit-level result is padded at the end with `0`s to some multiple of 8, for byte alignment.