using System;

public class AircraftStateMachine {
    public IAircraftState playState { get; set; }
    public IAircraftState crashState { get; set; }
    public IAircraftState prepareState { get; set; }
    public IAircraftState endState { get; set; }
    
    private IAircraftState currentState{ get; set;}


    private Aircraft aircraft;
    public AircraftStateMachine(Aircraft aircraft)
    {
        this.aircraft = aircraft;
        prepareState = new PrepareState(this);
        playState = new PlayState(this);
        crashState = new CrashState(this);
        endState = new EndState(this);
        currentState = prepareState;
    }

    public void Do()
    {
        currentState.Do();
    }
    
    public void Activate()
    {
        currentState.Activate();
    }
    
    public void Crash(){
        currentState.Crash();
    }
    
    public void Respawn(){
        currentState.Respawn();
    }
    
    public void EndGame(){
        currentState.EndGame();
    }

    public void SetState(IAircraftState state){
        currentState = state;
    }

    public Aircraft GetAircraft()
    {
        return aircraft;
    }
}