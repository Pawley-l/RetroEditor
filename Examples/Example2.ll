
count = 15
index = -1;

LOOP FOR count
	index = index + 1
	
	SetFillColour(10*index,10*index,10*index)
	
	CreateEllipse(100*index,100,100,100)
ENDLOOP
