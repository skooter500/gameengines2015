void setup()
{
  size(500, 500);
  
  GameObject ship;
 
  ship = new GameObject();
  ship.addComponent(new ShipDrawer(ship, 20.0f, 20.0f));
  ship.addComponent(new KeyMovement(ship, 'W', 'A', 'D'));
  
  ship.position.x = 100;
  ship.position.y = 100;
  
  gameObjects.add(ship);
  
  GameObject aiShip = new GameObject();
  
  aiShip.addComponent(new ShipDrawer(aiShip, 20.0f, 20.0f));
  aiShip.addComponent(new AIMovement(aiShip, 100));
  
  aiShip.position.x = 200;
  aiShip.position.y = 100;
  
  gameObjects.add(aiShip);
  
}

float timeDelta = 1.0f / 60.0f; 

boolean[] keys = new boolean[526];
ArrayList<GameObject> gameObjects = new ArrayList<GameObject>();

void draw()
{
  background(0);
  stroke(0, 255, 255);
  for(GameObject gameObject:gameObjects)
  {
    gameObject.update();
    gameObject.render();
  }
}

void keyPressed()
{ 
  keys[keyCode] = true;
}
 
void keyReleased()
{
  keys[keyCode] = false; 
}




