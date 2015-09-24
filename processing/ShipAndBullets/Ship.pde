class Ship extends GameObject
{
  // Fields
  float w;

  // Constructor
  Ship()
  {
    x = width * 0.5f;
    y = height * 0.5f;
    w = 50.0f;
    rot = 0.0f;
  }  
  
  // Methods
  void update()
  {
    lx = sin(rot);
    ly = - cos(rot);
    if (keys[LEFT])
    {
      rot -= 0.01f;
    }
    if (keys[RIGHT])
    {
      rot += 0.01f;
    }
    if (keys[UP])
    {
      x += lx;
      y += ly;
    }
    if (keys[DOWN])
    {
      x -= lx;
      y -= ly;
    }
    
    if (keys[' '])
    {
      Bullet b = new Bullet(x + lx * 25, y + ly * 25, rot);
      gameObjects.add(b);      
    }
    
    
  }  
  void render()
  {
    pushMatrix();
    float halfW = w * 0.5f;
  
    translate(x, y);
    rotate(rot);
    line(- halfW, halfW, 0, - halfW);
    line(halfW, halfW, 0, - halfW);
    line(- halfW, halfW, 0, 0);
    line(halfW, halfW, 0, 0);
    popMatrix();
  }
}
