extends Area2D

var laserFrame = 0;

var rot = 0;
func _ready():
	$AnimatedSprite.frame = laserFrame;
	$AnimatedSprite.rotation = rot;

func _on_LaserTimer_timeout():
	queue_free()
