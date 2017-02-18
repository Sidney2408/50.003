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
    public boolean checkExpire() {//Check when the object's HP reaches 0
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
    private String type;//Refers to Player's class, e.g. Archer, Warrior, Magician
    private int Atk;
    public Player(String name,String type) {//sets the different stats for each player class
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
        if (inRange(Player player)) {//Check if another player is caught in the range of the attack
            player.takeDamage(Atk);
            if (player.checkExpire()) {//Check if the player survives the attack.
                player.die();
            }
        }
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
    public void interact() {
        if (Environment env.isInteractive() && env.inRange()) {//Checks whether the environment is interactive and in range
            takeDamage(env.Atk);
            if (checkExpire()) {//Checks if the player is defeated once damage is taken.
                die();
            }
            env.takeDamage(1);
            if (env.checkExpire()) {//Checks if the enviroment is destroyed once damage is taken.
                env.destroy();
            }
        }
    }
    public boolean inRange(Player player) {//Checks if player is in range to attack another player
        if (type == "Archer") {
            if (player.getCoord()[0] - coord[0] <= 3) {
                return Boolean.TRUE;
            }
        }
        if (type == "Warrior") {
            if (player.getCoord()[0] - coord[0] <= 1) {
                return Boolean.TRUE;
            }
        }
        if (type == "Magician") {
            if (player.getCoord()[0] - coord[0] <= 5) {
                return Boolean.TRUE;
            }
        }
        return Boolean.FALSE;
    }
}
