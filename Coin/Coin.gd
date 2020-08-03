extends AnimatedSprite
var start_pos
var bounce_y
var momentum = Vector2(0,0)
const default_speed = 75
const gravity_vector = Vector2(0,2)
var rng = RandomNumberGenerator.new()
var friction = 0.5
var timer_remaining = true
func _ready():
	rng.randomize()
	position = Vector2(8,8)
	start_pos = position
	bounce_y = start_pos.y + 10 + rng.randf_range(-10,10)
	momentum = Vector2(0,1).rotated(rng.randf_range(45,135)) * default_speed
	$Timer.connect("timeout", self, "_on_Timer_timeout")



func _process(delta):
	if timer_remaining:
		if momentum.length() > 0.05:
			momentum += gravity_vector
			position += momentum * delta
			if position.y > bounce_y and momentum.y > 0:
				momentum = momentum.bounce(Vector2(0,-1)) * friction
		else:
			momentum = Vector2(0,0)
	else:
		global_position = lerp(global_position, Vector2(0,0) , 0.1)
	


func _on_Timer_timeout():
	timer_remaining = false
