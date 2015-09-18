class ShipDrawer extends GameComponent
{
  float w, h;
  float halfW, halfH;
  ShipDrawer(GameObject gameObject, float w, float h)
  {
    super(gameObject);
    this.w = w;
    this.h = h;
    
    halfW = w / 2.0f;
    halfH = h / 2.0f;
  }
  
  void render()
  {
    pushMatrix();
    translate(gameObject.position.x, gameObject.position.y); 
    rotate(gameObject.rot);
    line(-halfW, halfH, 0, -halfH);
    line(halfW, halfH, 0, -halfH);
    line(-halfW, halfH, 0, 0);
    line(halfW, halfH, 0, 0);    
    popMatrix();
  }  
}
