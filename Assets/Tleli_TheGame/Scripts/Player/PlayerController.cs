using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float velBase;
	public float tempRotacion;
	float velRotacion;
	public float velMax;                    //velocidad maxima que puede llegar en el aire.
	float velInicial;                      //valor para reiniciar al tocar ground.
	public float velSegundoSalto;          //valor para reducir velocidad en el segundo salto.
	public float aceleracion = 0.08f;      //aceleracion en el aire.
	private float vel;
	bool isMoving;
	[HideInInspector] public Vector3 velocidad;
	float _deltaVelocidad = 0f;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	public float Salto;
	float saltoInicial;                    //valor para reiniciar al tocar ground.
	public float segundoSalto;            //valor para reducir altitud en el segundo salto.
	public float gravedad;
	public bool isGrounded;
	private float saltoTimeCounter;
	public float saltoTime;
	private bool isJumping;

    public int canMove = 0;
    float rotacionDefault;


    int extraJumps;
	public int extraJumpsValue;

	[HideInInspector] public CharacterController characterController;
	public Transform cam;

	[HideInInspector] public Vector3 moveDir;
	[HideInInspector] public bool isDisplaced;
	PlayerDash dashCount;
	
	TleliAnimationController tleliAnimationController;
	TleliDeath tleliDeath;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		tleliDeath = GetComponent<TleliDeath>();
		tleliAnimationController = GetComponentInChildren<TleliAnimationController>();
		dashCount = GetComponent<PlayerDash>();
		extraJumps = extraJumpsValue;
		velInicial = velBase;
		saltoInicial = Salto;

        rotacionDefault = tempRotacion;
	}

	void Update()
	{
        


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocidad.y < 0)
		{
			velocidad.y = -10f;
			tleliAnimationController.LandTrigger();
		}

		float vertical = Input.GetAxisRaw("Vertical");
		float horizontal = Input.GetAxisRaw("Horizontal");

		if (isDisplaced == false) //si se dashea no se puede controlar la direccion hasta que termine y la gravedad no se crece por la duracion de este
		{ 
			//gravedad
			Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
			velocidad.y += gravedad * Time.deltaTime;

		//salto
		if(!tleliDeath.isDead)// Stop actions when Tleli is Dead. By Emil.
		{ 
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			GetComponent<FMODUnity.StudioEventEmitter>().Play();
			isJumping = true;
			saltoTimeCounter = saltoTime;
			velocidad.y = Mathf.Sqrt(Salto * -2f * gravedad);
			//leliAnimationController.JumpTakeOffTrigger();
				tleliAnimationController.JumpTakeOffbool(true);

			}
			//doble salto
			if (Input.GetButtonDown("Jump") && extraJumps > 0)
			{
				GetComponent<FMODUnity.StudioEventEmitter>().Play();
				isJumping = true;
				saltoTimeCounter = saltoTime;
				velocidad.y = Mathf.Sqrt(Salto * -2f * gravedad);
				extraJumps--;
			}

			else if (Input.GetButtonDown("Jump") && isGrounded && extraJumps == 0)
			{
				GetComponent<FMODUnity.StudioEventEmitter>().Play();
				isJumping = true;
				saltoTimeCounter = saltoTime;
				velocidad.y = Mathf.Sqrt(Salto * -2f * gravedad);
			}

		if (Input.GetButton("Jump") && isJumping == true)
		{

			if (saltoTimeCounter > 0)
			{
				velocidad.y = Mathf.Sqrt(Salto * -2f * gravedad);
				saltoTimeCounter -= Time.deltaTime;
			}
			else
			{
				isJumping = false;
			}
		}
		if (Input.GetButtonUp("Jump"))
		{
			isJumping = false;
		}

		characterController.Move(velocidad * Time.deltaTime);

		if (isJumping == true) // acelera la velocidad = se cubre mas distancia con salto
		{
			//velBase += aceleracion;
			if (isMoving == true) //acelerar solo si se mueve, problemas con el segundo salto
			{
				velBase += aceleracion * Time.deltaTime;
			} 
		}

		if (isJumping == true && extraJumps < extraJumpsValue)
		{
			Salto = segundoSalto;
			if (velBase >= velSegundoSalto)
            {
				velBase = velSegundoSalto;
            }
		}

		if (velBase > velMax) // para no exceder limite de velocidad xD
		{
			velBase = velMax;
		}
			}
			if (isGrounded == true) //regresa a tierra
		{
			velBase = velInicial;
			Salto = saltoInicial;
			extraJumps = extraJumpsValue;

				if (tleliAnimationController.CheckFallLoop())
				{
                    canMove = 10;
					tleliAnimationController.JumpLandTrigger();
				}
		}


            if (canMove > 0)
            {
                canMove -= 1;
                velBase = 1;
                tempRotacion = 10;
            }

            if (isJumping == true)
            {
                velBase = 5.5f;
            }

            else { tempRotacion = rotacionDefault; }

            vel = velBase;
		if(!tleliDeath.isDead)// Stop actions when Tleli is Dead. By Emil.
			{ 
		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velRotacion, tempRotacion);

			transform.rotation = Quaternion.Euler(0f, angle, 0f);

			moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			characterController.Move(moveDir.normalized * vel * Time.deltaTime);

				_deltaVelocidad = vel * Time.deltaTime;
			isMoving = true;

			tleliAnimationController.SetForwardSpeedParameter(1f);
		}

		if (direction.magnitude <= 0f)
		{
			isMoving = false;
			tleliAnimationController.SetForwardSpeedParameter(0f);
			}
		}
		}
		if (velocidad.y<0)
        {
			tleliAnimationController.JumpFallLoopBoolParameter(false);
			tleliAnimationController.JumpTakeOffbool(false);
		}

		if (velocidad.y>0)
        {
			tleliAnimationController.JumpFallLoopBoolParameter(true);
        }
		
        
	}

	public float GetVelocity()
	{
		return _deltaVelocidad;
	}

public float GetJumpVelocity()
	{
		return velocidad.y;
	}
}
