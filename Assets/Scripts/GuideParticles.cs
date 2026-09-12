using UnityEngine;

public class GuideParticles : MonoBehaviour
{
    public ProjectileController projectile;
    public TrailRenderer flightTrail;
    public ParticleSystem guideParticles;

    void Update()
    {
        bool docked = projectile.isDocked;

        flightTrail.emitting = !docked; // NEW — trail only accumulates while actually flying

        if (docked)
        {
            var current = projectile.currentCannon;
            if (current != null && current.nextCannon != null)
            {
                transform.LookAt(current.nextCannon.position);
                if (!guideParticles.isEmitting) guideParticles.Play();
            }
            else
            {
                if (guideParticles.isEmitting)
                {
                    guideParticles.Stop();
                    guideParticles.Clear();
                }
            }
        }
        else
        {
            if (guideParticles.isEmitting)
            {
                guideParticles.Stop(); // NEW — hide the direction hint while airborne
                guideParticles.Clear(); 
            
            }


         

        }
    }
}
