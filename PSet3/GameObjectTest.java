package Pset3;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

/**
 * Created by jimmy on 19/2/2017.
 */
public class GameObjectTest {
    Player player1 ;
    Player player2 ;
    Player player3 ;
    Player dummy;
    int[] loc1 = {0,0};
    int[] loc2 = {1,0};
    int[] loc3 = {-3,0};
    int[] loc4 = {2,0};

    @Before
    public void runBeforeEachTest()
    {
        System.out.println("setting up");
         player1 = new Player("Jun Lun","Archer",loc1);
         player2 = new Player("JCube","Warrior",loc2);
         player3 = new Player("Matthew","Magician", loc3);
         dummy = new Player("Sudipta", "Magician", loc4);
    }

    // tear-down method using @After
    // @After methods are run after each test
    @After
    public void runAfterEachTest()
    {
        System.out.println("setting down");
    }
    @Test
    public void getHP() throws Exception {
        //Check for all classes
        int hp1 = player1.getHP();
        int hp2 = player2.getHP();
        int hp3 = player3.getHP();
        boolean hpCorrect = ((hp1 ==80)&&(hp2 == 120)&&(hp3 == 60));
        assertEquals(true, hpCorrect);
    }

    @Test
    public void getCoord() throws Exception {
        //Just test the coordinates of one char since the method is standardized
        int[] coords = player1.getCoord();
        assertArrayEquals(loc1,coords);
    }

    @Test
    public void getSprite() throws Exception {
        String sprite = player1.getSprite();
        assertEquals("Archer", sprite);
    }

    @Test
    public void changeSprite() throws Exception {
        player2.changeSprite("Sheep");
        System.out.println(player2.getSprite());
        assertEquals("Sheep", player2.getSprite());
    }

    @Test
    public void checkExpire() throws Exception {
        player2.takeDamage(9999);
        assertEquals(true, player2.checkExpire());
    }

    @Test
    public void takeDamage() throws Exception {
        player1.takeDamage(10);
        assertEquals(70,player1.getHP());
    }

    @Test
    public void expire() throws Exception {
        player2.takeDamage(9999);
        assertEquals("expired sprite path",player2.getSprite());
    }

    @Test
    public void attack() throws Exception {
        player1.attack(dummy);
        assertEquals(45, dummy.getHP());

    }

    @Test
    public void attackAnimationTest() throws Exception {

        player1.atkAnimation("Archer");
        player2.atkAnimation("Warrior");
        player3.atkAnimation("Magician");
    }

    @Test
    public void getAttacked() throws Exception {
        dummy.getAttacked(10);
        assertEquals(50,dummy.getHP());
    }
//For all the tests involving moving, the initial coordinates are {2,0}
    @Test
    public void moveUp() throws Exception {
        dummy.move("Up");
        int[] place = {2,1};
        assertArrayEquals(place,dummy.getCoord());

    }
    @Test
    public void moveLeft() throws Exception {
        dummy.move("Left");
        int[] place = {1,0};
        assertArrayEquals(place,dummy.getCoord());
    }

    @Test
    public void moveJump() throws Exception {
        dummy.move("Jump");
        int[] place = {2,3};
        assertArrayEquals(place,dummy.getCoord());
    }

    @Test
    public void moveDown() throws Exception {
        //Initialise the coordinates at {2,1}
        dummy.move("Down");
        int[] place = {2,-1};
        assertArrayEquals(place,dummy.getCoord());
    }
    @Test
    public void moveRight() throws Exception {
        dummy.move("Right");
        int[] place = {3,0};
        assertArrayEquals(place,dummy.getCoord());
    }

    //Tests if the characters are in range: a true is returned if they are in range.
    @Test
    public void inRangeWarrior() throws Exception {
        //Bug found: originally the aboslute distance was not accounted for
        assertEquals(false, player2.inRange(player3));
    }
    @Test
    public void inRangeMage() throws Exception {
        //Bug found: originally the aboslute distance was not accounted for
        assertEquals(true, player3.inRange(player1));
    }
    @Test
    public void inRangeArcher() throws Exception {
        //Bug found: originally the aboslute distance was not accounted for
        assertEquals(true, player1.inRange(player3));
    }

}