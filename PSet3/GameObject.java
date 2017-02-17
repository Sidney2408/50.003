/**
 * Created by Matthew on 17/2/2017.
 */
public class GameObject {
    public int HP;
    public int[] coord;
    public String sprite;
        public int getHP() {
        return HP;
    }
    public int[] getCoord() {
        return coord;
    }
    public void changeSprite(String newSprite) {
        sprite = newSprite;
    }
    public boolean checkExpire() {
        if (HP <= 0) {
            return Boolean.TRUE;
        }
        return Boolean.FALSE;
    }
    public void takeDamage(int dmg) {
        HP -= dmg;
    }
}

class Player extends GameObject {
    private String name;
    private String type;
    private int Atk;
    public Player(String name,String type) {
        this.name = name;
        this.type = type;
        if (type == "Archer") {
            HP = 80;
            Atk = 15;
        }
        if (type == "Warrior") {
            HP = 120;
            Atk = 10;
        }
        if (type == "Magician") {
            HP = 60;
            Atk = 20;
        }
    }
    public void attack() {
        //may vary based on type
        atkAnimation(type);
    }
    public void atkAnimation(String type) {
        if (type == "Archer") {
            run("archer_attack.gif");
        }
        if (type == "Warrior") {
            run("warrior_attack.gif");
        }
        if (type == "Magician") {
            run("magician_attack.gif");
        }
    }
    public void move(String direction) {
        // need to set max limit for movement
        if (direction == "Left") {
            coord[0] --;
        }
        if (direction == "Right") {
            coord[0] ++;
        }
        if (direction == "Up") {
            coord[1] ++;
        }
        if (direction == "Down") {
            coord[1] --;
        }
        if (direction == "Jump") {
            coord[1] += 3;
        }
    }
    public void die() {
        if (checkExpire()) {
            changeSprite("dead_player.png");
        }
    }
}
