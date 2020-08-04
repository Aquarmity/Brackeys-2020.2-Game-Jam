extends Sprite


var score = 0

func _ready():
	$Label.text = String(score)


func add_to_score(new_score):
	score += new_score
	$Label.text = String(score)
