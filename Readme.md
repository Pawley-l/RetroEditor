# Advanced Software Engineering Assessed Work

## Requirments
<pre>
1 Assignment due 9/11/2020  
        1.1 Management  
                1.1.1 Appropriate unit tests set up  
                1.1.2 Documentated  
        1.2 Implementation  
                1.2.1 UI Conforming to specification  
                1.2.2 Command parsing, execution  
                1.2.3 Syntax Checking  
                1.2.4 Drawing Commands working e.g. shapes, colours and fills  
                1.2.5 Written with inheritance and design patterns  
2 Assignment due 4/1/2021
        2.1 Managemant  
                2.1.1 Professional use of version Control  
                2.2 Extended Language  
                2.2.1 Variable support  
                2.2.2 Loop support  
                2.2.3 Conditions  
                2.2.4 Methods support (With and without parameters)  
                2.2.5 Syntax checking  
        2.3 Design and Implementation  
                2.3.1 Use of factory design pattern  
                2.3.2 Use of additional design patterns  
                2.3.3 Code documented with XML  
                2.3.4 Exception Handling  
        2.4 Additional Functionality  
</pre>

## Programming Language Syntax & Funcs

### Objects
<pre>
rectangle **width**, **height** 
circle **radius** 
triangle **side**,**side**,**side**
</pre>

### Main Functions
<pre>
pen **colour**  
fill **on/off**  
moveTo **position**  
drawTo **position**  
clear  
reset  
</pre>

### Conditions
<pre>
if **condition**  
	~  
endif  
</pre>
### Loops
<pre>
loop  
	~~  
endloop  
</pre>
### Methods
<pre>
Method myMethod(**parameter list**)  
	~~  
Endmethod  

myMethod(<parameter list>)  

### Example
DrawTo x,y  
MoveTo x,y  
Circle **radius**  
Rectangle **width**, **height**  
Triangle **base**, **adj**, **hyp**  
Polygon [points,...]  

If <variable>=10  
	Line 1  
	Line 2  
Endif  

Radius = 20  
Width = 20  
Height = 20  
Count = 1  
Loop for Count  
	Circle radius  
	Radius+10  
	Rectangle width, height  
	Width+10  
	Height + 10  
	Count+1  
Endloop  
</pre>