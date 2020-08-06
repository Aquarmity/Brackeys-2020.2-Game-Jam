extends AnimatedSprite
var start_pos = Vector2(0,0)
var bounce_y
var momentum = Vector2(0,0)
const default_speed = 75
const gravity_vector = Vector2(0,2)
var rng = RandomNumberGenerator.new()
var friction = 0.5
var timer_remaining = true
onready var coin_node = get_parent().get_parent().get_node_or_null("Player/CanvasLayer/Sprite2")

func _ready():
	if coin_node == null:
		coin_node = get_parent().get_parent().get_parent().get_parent().get_node_or_null("Player/CanvasLayer/Sprite2")
	if coin_node == null:
		coin_node = get_parent().get_parent().get_parent().get_node("Player/CanvasLayer/Sprite2")
	
	rng.randomize()
	if start_pos == Vector2(0,0):
		position = Vector2(8,8)
	else:
		position += Vector2(8,8);
	start_pos = position
	
	bounce_y = start_pos.y + 10 + rng.randf_range(-10,10)
	momentum = Vector2(0,1).rotated(rng.randf_range(45,135)) * default_speed
# warning-ignore:return_value_discarded
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
		global_position = lerp(global_position, coin_node.global_position + Vector2(8,8) , 0.1)
		if (global_position - coin_node.global_position - Vector2(8,8)).length() < 2:
			coin_node.add_to_score(1)
			queue_free()
	


func _on_Timer_timeout():
	timer_remaining = false
