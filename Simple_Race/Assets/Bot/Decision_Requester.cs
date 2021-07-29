using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Unity.MLAgents{
    
    [RequireComponent(typeof(Agent))]

    public class Decision_Requester : MonoBehaviour{
        [Range(1,20)]
        [Tooltip("The frequency with which the agent requests a decision. A DecisionPeriod of 5 means that the Agent will request a decision every 5 Academy steps.")]
        public int decision_period = 3;
        [Tooltip("Indicates whether or not the agent will take an action during the Academy steps where it does not request a decision. Has no effect when DecisionPeriod is set to 1.")]
        [FormerlySerializedAs("RepeatAction")]
        public bool act_between_decisions = true;
        [NonSerialized]
        Agent m_agent;

        internal void  Awake() {
            m_agent = gameObject.GetComponent<Agent>();
            Debug.Assert(m_agent != null, "Agent not found");
            Academy.Instance.AgentPreStep += MakeRequests;
        }
        void MakeRequests(int academyStepCount){ // will agent make a decision through academy
            if (academyStepCount % decision_period == 0)  m_agent?.RequestDecision();
            if (act_between_decisions) m_agent?.RequestAction();
        }
         void OnDestroy(){if (Academy.IsInitialized) Academy.Instance.AgentPreStep -= MakeRequests;}
    }
}