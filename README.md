# Game of Life
<p>John Conway's cellular automation Game of Life written in C# with Unity.</p>

<p align="center">
  <img width="640" height="360" src="https://imgur.com/68TZ2p8.gif">
</p>

> Tags: C#, Unity
<p><b>Program logic is in GameOfLife/Assets/Scripts</b></p>

</br>

## Background
<p>John Conway first throught of the idea in 1970. The cells follow four simple rules:

1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
2. Any live cell with two or three live neighbours lives on to the next generation.
3. Any live cell with more than three live neighbours dies, as if by overpopulation.
4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
</p>

<br>
<p>From these simple instructions, very complex behaviour arises.</p>

<table class="multicol" role="presentation" style="border-collapse: collapse; padding: 0; border: 0; background:transparent; width:auto; margin:auto;"><tbody><tr>
<td style="text-align: left; vertical-align: top;">
<table class="wikitable">

<tbody><tr>
<th colspan="2">Still lifes
</th></tr>
<tr>
<td>Block
</td>
<td><img width="66" height="66" src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Game_of_life_block_with_border.svg/1024px-Game_of_life_block_with_border.svg.png">
</td></tr>
<tr>
<td>Bee-<br>hive
</td>
<td><img width="98" height="82" src="https://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Game_of_life_beehive.svg/98px-Game_of_life_beehive.svg.png">
</td></tr>
<tr>
<td>Loaf
</td>
<td><img width="98" height="98" src="https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Game_of_life_loaf.svg/98px-Game_of_life_loaf.svg.png">
</td></tr>
<tr>
<td>Boat
</td>
<td><img width="82" height="82" src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/Game_of_life_boat.svg/82px-Game_of_life_boat.svg.png">
</td></tr>
<tr>
<td>Tub
</td>
<td><img width="82" height="82" src="https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Game_of_life_flower.svg/82px-Game_of_life_flower.svg.png">
</td></tr></tbody></table>
<p> 
</p>
</td>
<td style="text-align: left; vertical-align: top; padding-left: 1em;">
<table class="wikitable">

<tbody><tr>
<th colspan="2">Oscillators
</th></tr>
<tr>
<td>Blinker<br>(period 2)
</td>
<td><img width="82" height="82" src="https://upload.wikimedia.org/wikipedia/commons/9/95/Game_of_life_blinker.gif">
</td></tr>
<tr>
<td>Toad<br>(period 2)
</td>
<td><img width="98" height="98" src="https://upload.wikimedia.org/wikipedia/commons/1/12/Game_of_life_toad.gif">
</td></tr>
<tr>
<td>Beacon<br>(period 2)
</td>
<td><img width="98" height="98" src="https://upload.wikimedia.org/wikipedia/commons/1/1c/Game_of_life_beacon.gif">
</td></tr>
<tr>
<td>Pulsar<br>(period 3)
</td>
<td><img width="137" height="137" src="https://upload.wikimedia.org/wikipedia/commons/0/07/Game_of_life_pulsar.gif">
</td></tr>
<tr>
<td>Penta-<br>decathlon<br>(period&nbsp;15)
</td>
<td><img width="89" height="145" src="https://upload.wikimedia.org/wikipedia/commons/f/fb/I-Column.gif">
</td></tr></tbody></table>
<p> 
</p>
</td>
<td style="text-align: left; vertical-align: top; padding-left: 1em;">
<table class="wikitable">

<tbody><tr>
<th colspan="2">Spaceships
</th></tr>
<tr>
<td>Glider
</td>
<td><img width="84" height="84" src="https://upload.wikimedia.org/wikipedia/commons/f/f2/Game_of_life_animated_glider.gif">
</td></tr>
<tr>
<td>Light-<br>weight<br>spaceship<br>(LWSS)
</td>
<td><img width="98" height="126" src="https://upload.wikimedia.org/wikipedia/commons/3/37/Game_of_life_animated_LWSS.gif">
</td></tr>
<tr>
<td>Middle-<br>weight<br>spaceship<br>(MWSS)
</td>
<td><img width="162" height="146" src="https://upload.wikimedia.org/wikipedia/commons/4/4e/Animated_Mwss.gif">
</td></tr>
<tr>
<td>Heavy-<br>weight<br>spaceship<br>(HWSS)
</td>
<td><img width="178" height="146" src="https://upload.wikimedia.org/wikipedia/commons/4/4f/Animated_Hwss.gif">
</td></tr></tbody></table>
<p> 
</p>
</td></tr></tbody></table>

<p align = "center">
  Credits to https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life for the above table.
</p>

</br>

## My Approach
<p>I use three 2D arrays to hold the current the current state of the cells, the next state of the cells, and the GameObjects to display on screen. By looping through the current states and updating the GameObjects and next states, the animation is achieved.</p>

<p>I chose to represent the living cells in two ways - either with a solid colour
or a colour gradient depending on how long the cell has been alive. You can toggle between
the two using the 'Colour' button at the bottom.</p>

<p>More implementations can be added by using the provided interface.</p>

| <a>**Single Colour**</a> | <a>**Dynamic Colour**</a> |
| :---: |:---:|
| <img width="300" height="300" src="https://imgur.com/YFV9rg5.png"> | <img width="300" height="300" src="https://imgur.com/r98NDlw.png"> |

</br>

## How to Play

- Use <a href="https://fontan10.itch.io/conways-game-of-life" target="_blank">**this link**</a> to run the simulation on your web browser or request a pull and run the game in Unity.

- Play around with the buttons and sliders at the bottom of the screen to adjust the simulation

<p align = "center">
  <img width="325" height="55" src="https://imgur.com/7nkuoxl.png"> <img width="325" height="55" src="https://imgur.com/3U8imOf.png">
</p>

- Click on screen to set the cells alive. If you don't see them come alive, try pausing the game or lowering the frames/s before you click on the screen.

</br>

## Extending the Code
<p>The simulation currently represents the living and dead cells in two ways.</p>

<p>If you want to add another respresentation of the cells, create a class that
implements interface IGridManagerable. Then add an instance of your class to
_gridManagers[] in Main.Start():</p>

```csharp
private void Start()
    {
        // some code

        _gridManagers = new IGridManagerable[]
        {
            new PolyColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") },
            new MonoColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") }
            
            // add your instance here
        };

        // some code
    }
```
<br>
<p>More button functions can also be added by creating a function in the Main class
and adding the function to your button.</p>
