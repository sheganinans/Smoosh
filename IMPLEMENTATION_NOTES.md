# Implementation Notes

## IP Fragmentation

The [MTU](https://en.wikipedia.org/wiki/Maximum_transmission_unit) defines the size of packet that can be sent before it is fragmented,
is guaranteed to be at least 1,500 bytes, and may be higher depending on network configuration.
When designing low latency network applications which send messages through the open internet, this limit is not controllable by
the application developer, and so it is important to implement these applications respecting this limit.

As such, Smoosh's encoder design focus is twofold:
Let users to encode messages larger than this limit, and optimize for the users that worry about the MTU.

How I see it, there are two types of users that I foresee using this library: Those that care about bandwidth, and those that care about
bandwidth *and* latency.

For the users that only care about bandwidth, there is good 'ol `Smoosh`.

## `Smoosh.Latency`

For users that care about latency: There is the `Smoosh.Latency` version of Smoosh.

The latency sensitive version has a few differences, mainly that it ignores attributes, and has a tighter encoding.
Finally, that the `encode` function will throw and exception if the message is larger than the the designated limits.
If you need to send packets larger than the `MTU` limit, and want the fastest encoding, and do not need to use attributes
then you may be interested in `mkMTUIgnoringEncoder`, which is demonstrated in [SAMPLES.md](./SAMPLES.md).

This design is aimed to serve the users who have strongly constrained latency demands, under the supposition that in a
well tested application, constructing a packet that unexpectedly disrespects the MTU, is an exceptional case.

As I mention in [IP Fragmentation](#ip-fragmentation), there are two similar, yet separate aims to this project.
I'd like to see Smoosh used as a general purpose library, and as a library used for low-latency communication.

The two pronged aim gives some tension to the design, because in the first frame you *do not* want arbitrary limitations put on the size of
objects you can encode, and have a whole bunch of nice features like attributes for your fields, that automatically improve
the encodings of your objects.
In the other frame, you want to be made *acutely aware* of these limitations, you don't want library to do fancy things behind
your back so that you can design your software in a way that respects these limits.

There isn't much more to say about the first frame, so let's consider the second. To accomodate, `Smoosh.Latency` will simply refuse, even 
`raise` an exception, when asked to encode an object larger than than the limits set forth.

## Minimum Viable Fanciness

`Smoosh.Latency` is purposely minimalistic in it's design, rejecting a few quality of life features in the mainline
version of `Smoosh`, on the premise that the user wants the highest power to weight ratio. It may be the case that in the future
I figure out a way to include some of the higher level features from `Smoosh` into `Smoosh.Latency`.

My current thoughts on the matter: Is that it's likely that `[<Utf8>]` can land into `Smoosh.Latency`, since it's rather
useful, the overhead of checking for that attribute is not too high relative to encoding or conversion, and it results
in 2x space saving. All very useful.

However with the [UTF8 String Proposal](https://github.com/dotnet/runtime/issues/933) ongoing, I feel like in a new
version of .Net, the recommendation would be to just use this new type. However, of course this now makes the library
dependent on very recent features, breaking compatibility for essentially most-to-all legacy users. 
So I opted for what I consider the least-worse option, and simply did nothing, and suggest against heavy use of strings.

## Rule of 3

Currently the codebase is designed in such a way, that the hot path for `Smoosh.Latency` is entirely separate from `Smoosh`, I would
suppose that sentence belies the design decision, and I'll write a few more words. This does involve some "costs", namely there is some
code repetition in the two implementations. However I think this is a fair cost to pay to avoid premature abstraction, since I strongly
believe in the Rule of 3 Before Abstraction (Ro3BA). That said, if there is a compelling case for a third alternative to this library,
likely relative to the above mentioned missed use cases, then I'll take the abstraction consideration more seriously.