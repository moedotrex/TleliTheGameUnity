EnemyFX, Karime y Lucy, ver 01
--------------------------------

** Se necesita instalar Visual Effect Graph desde el Package Manager **

Se implementaron efectos cuando Tleli le pega con su espada a un enemigo. Se usaron 2 efectos diferentes: un particle system que debe ser hijo del enemigo y un Visual Effect Graph que debe ser hijo de la espada

-El prefab "Sparks" debe ser hijo de Player_Weapon (Player>Sword>Player_Weapon)
-El script "AttackFX" debe ser componente de Player Weapon

-El prefab "EnemyParticleSys" debe ser hijo de un Enemy

-Se hicieron modificaciones en los scripts "EnemyHealth" (se cambió variable de currentHealth de private a public)
y a SwordAttack (para comunicación con el script de AttackFX).










