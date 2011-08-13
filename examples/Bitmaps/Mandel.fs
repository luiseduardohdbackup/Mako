######################################################
##
##  Mandel:
##
##  Another bitmapped graphics demo, drawing a
##  Mandelbrot fractal using fixed-point math.
##
##  John Earnest
##
######################################################

:array sprites       1024 0
:array sprite-tiles 81920 0 (320x256 pixel buffer)

:include "../Sprites.fs"

: pixel ( x y -- address )
	2dup 64 / 5 * swap 64 / + 4096 *
	swap 64 mod 64 * +
	swap 64 mod +
	ST @ +
;

: init-fullscreen ( -- )
	19 for
		64x64 i
		i 5 mod 64 * # x
		i 5 /   64 * # y
		i >sprite
	next
;

: ^2  dup * 4096 / ;         (x   -- x^2)
: nx  ^2 swap ^2 swap - ;    (x y -- nx)
: ny  * 2 * 4096 /      ;    (x y -- ny)
: in  ^2 swap ^2 + 16384 < ; (x y -- flag)

: mandel ( a b -- color )
	>r >r 32 >r 0 0
	loop
		2dup nx k + >r
		ny k + r> swap
		r> 1 - >r
		2dup in i 0 > and
	while
	r> rdrop rdrop >r 2drop r>
;

: main

	init-fullscreen

	239 for
		319 for
			j 34 * 4096 -
			i 44 * 10240 -
			mandel
			dup 1536 * swap 524288 * or 0xFF000000 or
			i j pixel !
		next
	next

	loop
		sync
	again
;