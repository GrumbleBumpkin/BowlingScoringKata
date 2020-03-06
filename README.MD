# Bowling Scoring Kata

## Command Line Use

You can parse a comma-separated string of integers as a command line argument:

```
$ ./BowlingScoringKata 10,10,10,10,10,10,10,10,10,10,10,10

>         Game 1: 300 points
```

You can parse a csv file containing multiple sets of comma-separated integers:

```
$ ./BowlingScoringKata /path/to/your/test.csv

> CSV File Line 1
>         Game 1: 300 points
>         Game 2: 59 points (Unfinished with last score in frame 5)
> CSV File Line 2
>         Game 1: 0 points
```

## Interactive Use

Run the BowlingScoringKata executable to have the following options:

- **i**: Interactively enter single scores as they are made and track a game for a single player.
- **s**: Parse a string of comma-separated integers. Loop can be exited by typing `exit`.
- **f**: Provide a file path to a CSV file containing rows of integers. Loop can be exited by typing `exit`.
