
How the implementation makes efficient use of computer hardware:
•The project utilizes multithreading by Scheduling jobs in parallel
•Separates data from logic to make it faster to look up pointers for the computer
•Systems schedule jobs instead of just running them and the jobs are picked up by the workers


How the implementation uses a data-oriented method
•Components are created to only store data and assigned to different entities
•Systems use those components and created from them aspects to perform operations on entities
•Components store only easy to read data like floats


How code was optimized by based on findings from using a profiler
•I actually misunderstood what it means to work in ECS and I tried to make the project without DOTS first and just made data scripts and functions scripts and then I had different lists of all the 
the gameObjects that had same componenets and then I had gameobjects that needed certain components to provide operations. When I had about 300 gameobjects on the screen I had a lot of spikes.
In addition to that, Spawning and Destorying objects were creating small spikes
•When I switched to actually working in DOTS, I immidiately started separating systems so I haven't seen many spikes on the profiler.