/**
 * Created by Matthew on 17/2/2017.
 */

/**
 * <h1>Game Object</h1>
 * This is the parent class for all objects in game.
 * Main attributes include HP, coordinates and sprite.
 * 
 * @author Yak Jun Lun
 */
public class GameObject {
    public int HP;
    public int[] coord;
    public String sprite;
    /**
     * Returns the HP of object.
     * @return HP 
     */
    public int getHP() {
        return HP;
    }
    /**
     * Returns the coordinates of the object.
     * @return [x,y]
     */
    public int[] getCoord() {
        return coord;
    }
    /**
     * Returns the link to the corresponding sprite.
     * @return 
     */
    public String getSprite(){
        return "point to sprite folder + sprite name";
    }
    /**
     * Used to switch to different sprites.
     * @param newSprite 
     */
    public void changeSprite(String newSprite) {
        sprite = newSprite;
    }
    /**
     * Check if the HP is less than zero. If it is, run the expire method.
     */
    public void checkExpire() {//Check when the object's HP reaches 0
        if (HP <= 0) {
           expire();
        }
    }
    /**
     * Reduces HP by specified amount 
     * @param dmg 
     */
    public void takeDamage(int dmg) {
        HP -= dmg;
    }
    /**
     * Runs the script to destroy object as well as expire sprite.
     */
    public void expire(){
        changeSprite("expired sprite path");
    }
}
/**
 * Class for players
 * @author Yak Jun Lun
 */
class Player extends GameObject {
    /**
     * Players username
     */
    private String playerName; //Player's username
    /**
     * Class/ character that player chooses
     */
    private String type;//Refers to Player's class, e.g. Archer, Warrior, Magician
    private int atk;
    private int orientation;
    private int range;
    /**
     * constructor that initialises upon gameplay
     * @param name
     * @param type 
     */
    public Player(String name,String type,int[] coord) {//sets the different stats for each player class
        this.playerName = name;
        this.type = type;
        this.coord = coord;
        if (type.equals("Archer")) {
            HP = 80;
            atk = 15;
            this.sprite = type;
            range = 5;
        }
        if (type.equals("Warrior")) {
            HP = 120;
            atk = 10;
            this.sprite = type;
            range = 1;
        }
        if (type.equals("Magician")) {
            HP = 60;
            atk = 20;
            this.sprite = type;
            range = 5;
        }
    }
    /**
     * calls method to create hitbox in front of character depending on range
     * @return 
     */
    public int[] attack() {
        //may vary based on type
        atkAnimation(type);
        int[] atkCoord = null;
        atkCoord[0] = this.getCoord()[0]+(orientation*range);
        atkCoord[1] = this.getCoord()[1];
        return atkCoord;
        /*
        if (inRange(Player player)) {//Check if another player is caught in the range of the attack
            player.takeDamage(Atk);
            if (player.checkExpire()) {//Check if the player survives the attack.
                player.die();
            }
        }
        */
    }/**
     * calls animation sprite
     * @param type 
     */
    public void atkAnimation(String type) {
        if (type.equals("Archer")) {
            System.out.println("run Archer attack sprite");
        }
        if (type.equals("Warrior")) {
            System.out.println("run warrior attack sprite");
        }
        if (type.equals("Magician")) {
            System.out.println("run Magician attack sprite");
        }
    }/**
     * calls sprite to get damaged
     * and method to reduce HP
     * @param atk 
     */
    public void getAttacked(int atk){
        System.out.println("run damaged animation");
        takeDamage(atk);
        
    }/**
     * move according to button press, also changes orientation
     * @param direction 
     */
    public void move(String direction) {
        // need to set max limit for movement
        if (direction.equals("Left")) {
            this.orientation = -1;
            coord[0] --;
        }
        if (direction.equals("Right")) {
            this.orientation=1;
            coord[0] ++;
        }
        if (direction.equals("Up")) {
            coord[1] ++;
        }
        if (direction.equals("Down")) {
            coord[1] --;
        }
        if (direction.equals("Jump")) {
            coord[1] += 3;
        }
    }
   
    
    /**
     * check for range. Might be handled in game engine instead tho
     * @param player
     * @return 
     */
    public boolean inRange(Player player) {//Checks if player is in range to attack another player
        if (type.equals("Archer")) {
            if (player.getCoord()[0] - coord[0] <= 3) {
                return Boolean.TRUE;
            }
        }
        if (type.equals("Warrior")) {
            if (player.getCoord()[0] - coord[0] <= 1) {
                return Boolean.TRUE;
            }
        }
        if (type.equals("Magician")) {
            if (player.getCoord()[0] - coord[0] <= 5) {
                return Boolean.TRUE;
            }
        }
        return Boolean.FALSE;
    }
    /*
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

*/
}
