# 1 Introduction
Go & Ground (GnG) is a pseudo game developed in Unity for the MMI 706 Reinforcement Learning course. Aim is to develop the game and use different reinforcement learning methods to train agents in the game.

Game represents a battle scene with the soldiers of 10 vs. 10. In many action games there are scenes where player and enemy armies battle. In these battles player have limited control over the soldiers, giving them some orders (stop, go this direction, retreat etc.) but mostly NPCs (Non-player characters) act on themselves based on their predetermined AIs.

The main idea to create NPCs with different success rates, trained by reinforcement learning for these kinds of scenes.

This report is structured as follows: Part 2 Background provides required reinforcement learning information for the project. Part 3 Game Development describes the game and how game interacts with the RL algorithms. Part 4 Training explains the training types. Part 5 explains the results of the project and its discussion. Finally, Part 6 gives the limitation of the project.

# 2 Background

## 2.1 Reinforcement Learning
Reinforcement Learning mainly consists of three elements: agent, environment and reward. When agent takes an action, it causes a change in the environment and  environment return a reward to the agent. Agent aims to maximize its cumulative reward function using different algorithms. Games inherently consist reward functions due to their competitive nature. Therefore, they can be easily used for the reinforcement learning studies.

## 2.2 Reinforcement Learning Application on Games

 Reinforcement Learning algorithms are being used for the playing different games from classical turn based tabletop games (chess, backgammon [^tavla], go[^go]) to the early video games like different Atari games[^atari], Super Mario Bros, Zelda. Different environments like ELF [^elf] ,SC2LE[^sc2]  and MicroRTS [^microrts] are developed for the reinforcement learning studies on realtime strategy games. In this project a new game has been developed in Unity and trained with the Unity ML Agents.

##  2.3 Unity ML-Agents
Unity ML-Agents Toolkit is developed by the Unity for the machine learning applications and research on Unity game engine environment [^unity]. In this environment single or multiple agents can be trained with different methods. Later these trained agents can be used in the games as NPCs or creating new challenges for the players. Environment provides a PyTorch  integration which is an open source machine learning framework[^pytorch]. Unity ML-Agents version 2.1.0 is used in this project. 

Unity ML-Agents consist of the following parts:

* Learning Environment: It is the game environment designed in Unity.  It may include any game object that a game developer designed. It should include Agent(s), their targets and challenges.
* External Communicator: It provides the connection between **Learning Environment** and the **Python Low-Level API**.
* Python Low-Level API:  API that allows the control of the Unity Learning Environment through the Python.
* Python Trainer: This includes machine learning algorithms for the training of the agents. Several different algorithms are already included in this package.

![Pasted image 20210702021551](https://user-images.githubusercontent.com/81522783/158061347-9683e986-2cef-491d-aed6-828e85fa04ce.png)

Learning Environment's main two components are:

* Agents: This component is attached to desired agent game objects. It contains observations, actions and possible rewards.
* Behavior: This includes behavior of the agents through the training or after the training.


## 2.4 Reinforcement Learning Algorithms
Unity ML-Agents includes two algorithms: Proximal Policy Optimization (PPO) [^ppo] and Soft Actor Critic (SAC) [^sac]. Both of these algorithms are used in the training of the agents.

### 2.4.1 Proximal Policy Optimization (PPO)

PPO is and on-policy algorithm which uses the policy gradient method. It is stated that PPO performs better than than the A2C or similar algorithm on the continuous control tasks[^ppo]. Pseudo code of PPO is as follows:

![Pasted image 20210702023340](https://user-images.githubusercontent.com/81522783/158061362-7dc318aa-88b1-4bed-8acb-507ec0746faf.png)

### 2.4.2 Soft Actor Critic (SAC)

SAC is an off policy, actor-critic deep reinforcement learning algorithm and it is stated that SAC is more stable algorithms than the other off policy algorithms[^sac]. Pseudo code of SAC is as follows:

![Pasted image 20210702023340](https://user-images.githubusercontent.com/81522783/158061367-0063ebe4-3d17-40c9-8e61-70cc605064cc.png)

# 3 Game Development
GnG game consists of two teams (blue & red), each has 10 players. When one team lost all its members or no one dies over a minute, game ends and team with the most members win the game. Each member belongs to one of 3 classes and they have a rock-paper-scissors relationship between them.

## 3.1 Game Environment 
Pawns class has the following information about the soldiers:

* **Health:** Total health of the soldier. Each soldier will start with the maximum health and game object will be destroyed when they reach the 0.
* **Type:** Each soldier belong the one of three types (Rock, Paper, Scissors)
* **Team:** Blue or red

Each type have an advantage or disadvantage against another type (showed in the below). When two members collider:

* Advantageous soldier: -1 HP
* Disadvantageous soldier: -2 HP
* Same type: -0.5 HP

![Pasted image 20210625154306](https://user-images.githubusercontent.com/81522783/158061371-62f984c0-3657-4a2c-ac4c-7fd070d03d3d.png)

Two simple rule based algorithms are prepared for the movement of the members. These movements will be used for training and testing of the reinforcement learning agents.

* **Seeker Movement**: Randomly seek and follow an enemy member.
* **Hunter Movement**: Seek and follow enemy prey member. 

**Game scene** have a border but members can pass these lines but when they passed the border they rapidly lose health points.

## 3.2 Reinforcement Learning Environment
### 3.2.1 Sensory Inputs

To play the game, agents need to know the following information about the environment:

* Agent
	* Position
	* Type
* Enemy
	* Position
	* Type

Positions are stored in Unity Vector3 classes which consist of x,y and z coordinates of the game object however, y coordinates of the all objects are the same and therefore unrequired. After the initial trial, only the x and z coordinates of the members are stored. This change increased the performance of the convergence.

Agent's own location is stored as global direction however when we apply the 
same logic to the position of enemy members, all agents shows the same movements regarding of their initial positions. To solve this problem, enemy members' positions are stored relative to the agents' own positions (enemy position-agent position). Relative distance of the agent and enemy is more important than the global positions of them and it's easier to reach a result from the relative direction, because *rewards* will be called when these two objects are close to each other.

All members belong to one of three different types: rock, scissors and paper. This information is also crucial for the selection of the action because each type has an advantage and disadvantage against one of the other types.    

```cs
        sensor.AddObservation((transform.localPosition.x + xRange));
        sensor.AddObservation((transform.localPosition.z + zRange));
        sensor.AddObservation(GetComponent<Pawns>().typeNo);

        for (int i = 0; i < gSet.bList.Length; i++)
        {
            if (gSet.bList[i] == null)
            {
                sensor.AddObservation(0);
                sensor.AddObservation(0);
                sensor.AddObservation(0);
            }
            else
            {
				sensor.AddObservation((gSet.bList[i].transform.localPosition.x -
				transform.localPosition.x));				
				sensor.AddObservation((gSet.bList[i].transform.localPosition.z -
				transform.localPosition.z));
				sensor.AddObservation(gSet.bList[i].GetComponent<Pawns>
				().typeNo);
            }
        }

```

### 3.2.2 Control
Agents have two continuous actions controlled by the reinforcement learning algorithms, their positions on x and z. 

```cs
    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(Time.deltaTime * 
		controlSignal.x * speed, 0, Time.deltaTime * controlSignal.z * speed);
	}
```

### 3.2.3 Rewards
Two different reward system is prepared for the training. First one is intermediate reward system, in that system agents get rewards for their short term actions in the game which will result to win the game. However, end game reward system only provides rewards for winning the game or losing the game. I expected that latter will be much harder to reach because agents will not get any feedback about their actions up until game is end. 
Intermediate reward system assigns different rewards to different agents however endgame reward system assign same reward to all agents.

**Intermediate Reward System**

* +0.01: Touching prey type enemy
* -0.01: Touching predator type enemy
* +0.005: Touching same type enemy
* -1: Leaving the scene
```cs
 if (figth.type2 == preyType && figth.obje2Team == figth.enemyTeam)
        {
            AddReward(0.01f);
        }
        else if (figth.type2 == predatorType && figth.obje2Team == figth.enemyTeam)
        {
            AddReward(-0.01f);
        }
	  	else if (figth.type2 == notrType && figth.obje2Team == figth.enemyTeam)
        {
            AddReward(0.005f);

        }
```
**Endgame Reward System**

* +1: Winning the game
* -1: Losing the game
```cs
 if (enemyTeam == "blue")
        {
            //Game ending conditions:
            if (gSet.blueList.Count < 1) //win the round
            {
                SetReward(1);
                EndEpisode(); 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (gSet.redList.Count < 1) //lost the round
            {
                SetReward(-1);
                EndEpisode();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
```

# 4. Training 
Two mentioned algorithms (SAC, PPO) are used for the training against a team with the *Seeker* movement. Agent class with the same behavior assigned to each member of the training team. Both teams have the equal number of members (10). Each episode continue until one of the team losses all its members or when the time limit is reached. Agents with PPO algorithms trained for 500k steps. It is stated that SAC is more sample-efficient than PPO [^unity] therefore less number of steps (250k) used for the SAC training.

SAC algorithm is also used for the self-play training. In this method, members of the both team are consist of agents. Only difference is they have assigned to different teams. 

![Pasted image 20210702033807](https://user-images.githubusercontent.com/81522783/158061381-0a8d8090-c4c7-4b7d-b66e-ee3a0ac1ec01.png)


These three different training / algorithm types are trained with both explained reward systems (intermediate and end game). In total, 6 sets of agents are trained.

![Pasted image 20210702031108](https://user-images.githubusercontent.com/81522783/158061384-314d9cf6-7da9-41a1-9527-f257d2e1aaf7.png)


The following shows the Unity interface for the defining behavior parameters of an agent.

![Pasted image 20210702032807](https://user-images.githubusercontent.com/81522783/158061388-3bcde68c-9186-4829-a983-49fc30542ca1.png)




# 5. Results & Discussion

After the training, to understand the success of the agents, they have tested against the hardcoded AIs. Following table and figure shows the average success rate of each agent for over 100 rounds against the hardcoded AIs.

![Pasted image 20210702035813](https://user-images.githubusercontent.com/81522783/158061396-210ae1dd-9aa5-4ef7-9024-e526a6ea47f4.png)

![Pasted image 20210702035820](https://user-images.githubusercontent.com/81522783/158061439-c735039f-e4ae-47d1-8758-c76b8ead695d.png)

The main difference occurs between the intermediate reward system and end game reward system. Agents trained with end game reward system sadly do not win any game. I was already expecting that intermediate system will have better results than end game results, however I was not expecting zero success. I think main reason is rewards assigned to agents at the end of game and it's hard to find the behavior that caused that reward. There is already lots of "incorrect" actions between "correct" actions and reward, and it's hard to evaluate this system.

I have not provided information about the health levels of the enemy member to the agent. I think providing this information may increase the success rate of end game reward systems. Also providing *remaining* number of enemy team and ally team members may also help the agents in the learning.

The following image shows the cumulative reward vs. step graphic of three intermediate rewarded agent. Steep increase at the first steps are where agents learn not to fall down. They rapidly learn not to fall down, but after that all of agents do not change any significant increase in the cumulative reward. 
In the graph PPO shows slightly better cumulative rewards than other methods, however its success rate (31%) is the same with the Self-Play (SAC). I think the reason of this discrepancy is reward system does not exactly represent the game winning behaviors.

![Pasted image 20210702041218](https://user-images.githubusercontent.com/81522783/158061449-d09993ee-1e37-412a-9620-d8b92cafcdd4.png)

The following figure shows cumulative reward vs. step graphic of the end game rewarded agents. These rewards are closer to the 0 because they get -1 for the losing and +1 for winning the game. Self play agent may be look like the most successful of the three, however its higher cumulative rewards are earned against itself, not against a better algorithm. 

![Pasted image 20210702041946](https://user-images.githubusercontent.com/81522783/158061451-bb2d8940-31fd-4dbf-b233-7b5a5b479814.png)

# 6. Limitations
Unity ML-Agents have Python API support and allows users to use custom reinforcement learning algorithms however it's only limited to one agent. Therefore, I could not use it.

## 6.1 Lesson Learned

I have also tried to train only one agent in the environment; however, it was not very successful because one agent's effect on the whole scene is insignificant, it does not cause any game changing move.

When agents trained against the team of **hunters**, their learning rate is slower because they rapidly lose the game without doing any sensible movement. And when they trained against the steady enemy, it takes time to touch any enemy member therefore learning gets slower. After some trials I have found that fighting against the **seekers** is the best method for this project.

I have also learned that carefully fine tuning of RL parameters is really important for the convergence and convergence time. I have try to have optimum randomness for this environment.



[^atari]: Mnih, V., Kavukcuoglu, K., Silver, D., Graves, A., Antonoglou, I., Wierstra, D., & Riedmiller, M. (2013). Playing Atari with Deep Reinforcement Learning. 1–9. http://arxiv.org/abs/1312.5602
[^go]: Silver, D., Schrittwieser, J., Simonyan, K., Antonoglou, I., Huang, A., Guez, A., … Hassabis, D. (2017). Mastering the game of Go without human knowledge. Nature, 550(7676), 354–359. https://doi.org/10.1038/nature24270
[^tavla]: Tesauro, G. (2002). Programming backgammon using self-teaching neural nets. Artificial Intelligence, 134(1–2), 181–199. https://doi.org/10.1016/S0004-3702(01)00110-2
[^pytorch]: https://pytorch.org/
[^sc2]: Vinyals, O., Ewalds, T., Bartunov, S., Georgiev, P., Vezhnevets, A. S., Yeo, M., … Tsing, R. (2017). StarCraft II: A New Challenge for Reinforcement Learning. Retrieved from http://arxiv.org/abs/1708.04782
[^microrts]: https://github.com/santiontanon/microrts
[^elf]: Tian, Y., Gong, Q., Shang, W., Wu, Y., & Zitnick, C. L. (2017). ELF: An Extensive, Lightweight and Flexible Research Platform for Real-time Strategy Games. 1–14. http://arxiv.org/abs/1707.01067
[^ppo]: Schulman, J., Wolski, F., Dhariwal, P., Radford, A. & Klimov, O. Proximal policy optimization algorithms. Preprint at https://arxiv.org/abs/1707.06347v2 (2017).
[^sac]: Haarnoja, T., Zhou, A., Abbeel, P., & Levine, S. (2018). Soft actor-critic: Off-policy maximum entropy deep reinforcement learning with a stochastic actor. 35th International Conference on Machine Learning, ICML 2018, 5, 2976–2989.
[^unity]: Juliani, A., Berges, V., Teng, E., Cohen, A., Harper, J., Elion, C., Goy, C., Gao, Y., Henry, H., Mattar, M., Lange, D. (2020). Unity: A General Platform for Intelligent Agents. _arXiv preprint [arXiv:1809.02627](https://arxiv.org/abs/1809.02627)._ [https://github.com/Unity-Technologies/ml-agents](https://github.com/Unity-Technologies/ml-agents).
