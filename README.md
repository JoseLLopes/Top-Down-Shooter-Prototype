# 🔫 TopDown Shooter 3D - Unity

Veja o arquivo [LICENSE](LICENSE) para detalhes.


## ✅ CheckList Completo - TopDownShooter3D

### 🧰 1. Estrutura Inicial do Projeto
- [x] Criar projeto Unity 3D
- [x] Criar pastas: Scripts, Prefabs, Scenes, UI, Materials, Animations, Models
- [x] Criar cena principal GameScene e salvar em /Scenes/

### 🎮 2. Jogador
- [x] Criar GameObject "Player" com:
- [x] WASD para mover
- [ ] olhar/mirar com o mouse
- [x] Criar sistema de tiro:
  - [x] Instancia projéteis
  - [x] Adiciona tempo de recarga
  - [x] Define alcance, dano e cooldown

### 🎯 3. Inimigos (IA)
- [ ] Importar modelo 3D simples ou capsule + cor
- [ ] Adicionar NavMeshAgent
- [ ] Criar script EnemyAI.cs:
  - [ ] Segue o jogador
  - [ ] Dano por contato ou ataque de perto
- [ ] Criar sistema de vida e morte do inimigo
- [ ] Criar sistema de spawn por wave

### 💥 4. Tiro / Combate
- [ ] Criar prefab de "Projectile" com:
  - [ ] Collider
  - [ ] RigidBody
  - [ ] Script Projectile.cs com tempo de vida
- [ ] Criar sistema de dano ao colidir com inimigo
- [ ] Adicionar efeitos (som ou partículas)

### 💡 5. Sistema de Partidas
- [ ] Script GameManager.cs com:
  - [ ] Controle de waves
  - [ ] Pontuação
  - [ ] Spawn controlado de inimigos
  - [ ] Fim de jogo quando vida = 0

### 📊 6. UI / HUD
- [ ] Mostrar:
  - [ ] Vida do jogador
  - [ ] Munição (se houver)
  - [ ] Score
  - [ ] Tela de Game Over e botão de restart

### 🎨 7. Polimento visual
- [ ] Adicionar partículas (tiros, impacto, morte)
- [ ] Adicionar luzes e sombras
- [ ] Ajustar materiais e cores pra dar “cara de jogo”

### 📷 8. Apresentação
- [ ] Capturar 1 a 3 screenshots em /Screenshots/
- [ ] Exportar build para WebGL ou PC (/Build/)
- [x] Adicionar ao GitHub com README + LICENSE
- [ ] Escrever no README o que você desenvolveu, o que aprendeu e onde usou assets externos