class Bullet extends GameObject
{
  // Fields
  float w;
   float speed;
  boolean alive;
  
  Bullet(float x, float y, float rot)
  {
    this.x = x;
    this.y = y;
    speed = 10.0f;
    this.rot = rot;
  }
  
  void update()
  {
    lx = sin(rot);
    ly = - cos(rot);
    x += (lx * speed);
    y += (ly * speed);
    
    // Set the alive flag to be false if the bullet moves outside the window
    if (x < 0 || x > width || y < 0 || y > height)
    {
      alive = false;
    }
  }
  
  void render()
  {
    pushMatrix();
    translate(x, y);
    rotate(rot);
    line(0, -5, 0, 5);
    popMatrix();
  }
}
