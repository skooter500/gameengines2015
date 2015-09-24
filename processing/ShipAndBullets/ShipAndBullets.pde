void setup()
{
  size(500, 500);
  ship = new Ship(); // How to construct instances
  gameObjects.add(ship);
}

float speed = 2;
boolean[] keys = new boolean[526];

Ship ship; // ship is an instance of Ship
ArrayList<GameObject> gameObjects = new ArrayList<GameObject>();

void draw()
{
  background(0);
  stroke(255);
    
  for (int i = gameObjects.size() - 1 ; i >= 0  ; i --)
  {
    GameObject b = gameObjects.get(i);
    b.update();
    b.render();
    if (! b.alive)
    {
      gameObjects.remove(b);
    }
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
