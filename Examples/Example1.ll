
DrawTo(5,5)
MoveTo(1,1)

METHOD define(radius, rectangle, triangle)
	Circle(radius)
	Rectangle(rectangle, rectangle)
	Triangle(triangle, triangle, triangle)
ENDMETHOD

variable = 10

IF variable=10
	print("equal to 10)
ENDIF

variable = 20

Count = 1

LOOP FOR Count
	variable = variable + 10
ENDLOOP

define(variable, variable. variable)
