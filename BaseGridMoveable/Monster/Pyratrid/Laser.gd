extends Area2D


var rot = false
func _ready():
	if rot == false:
		$Sprite.rotation = 0
	elif rot == true:
		$Sprite.rotation = PI/2

func _on_LaserTimer_timeout():
	queue_free()
