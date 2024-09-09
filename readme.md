# Pomodoro Timer - Aplicação WPF

## Descrição do Projeto
Este projeto consiste em uma aplicação de **Pomodoro Timer** desenvolvida utilizando a tecnologia **WPF (Windows Presentation Foundation)**. A técnica Pomodoro é uma metodologia de gerenciamento de tempo, que alterna períodos de foco intenso com pequenos intervalos, para aumentar a produtividade e melhorar a concentração.

A aplicação permite ao usuário configurar tempos de trabalho (pomodoro), intervalo curto e intervalo longo, além de fornecer notificações visuais e sonoras para indicar o término de cada sessão.

## Funcionalidades

- **Configuração Personalizada**:
  - Definir a duração dos intervalos curtos e longos.
  - Escolher o número de ciclos antes do intervalo longo.
  
- **Contador por áudio**:
  - O indicador sonoro contribui para educar o seu cerébro de forma a memorizar a duração dos ciclos.

- **Notificações**:
  - Notificação sonora ao fim de cada ciclo

## Estrutura do Projeto

- **Camada de Apresentação (UI)**:
  - Criada usando XAML para definir a interface gráfica com botões e labels.
  - Estilo moderno e minimalista para facilitar o uso durante o trabalho.

- **Lógica de Negócio**:
  - Implementada em C# com classes que gerenciam o funcionamento do timer, alternância entre ciclos e a interação do usuário com o sistema.
  
## Tecnologias Utilizadas
- **WPF**: Framework utilizado para o desenvolvimento da interface gráfica.
- **C#**: Linguagem de programação utilizada para implementar a lógica de negócio.

## Próximos Passos
- A ser definido.

## How to Download and Build

1. **Clone o repositório (Se faze necessário ter acesso ao MSbuild)**:
   ```bash
   git clone https://github.com/MuilaerteJunior/Pomodoro/Pomodoro.git   

2. **Crie a pasta para o destino final do executável**:
    ```bash
   mkdir c:\MyPomodoro
   
3. **Localize a sua instalação do MSbuild para conseguir compilar a solução**:
   ```bash   
   msbuild Pomodoro.sln -p:Configuration=Release;OutDir=C:\MyPomodoro
   
4. **Invoque o executável gerado anteriormente**:
   ```bash
   c:\MyPomodoro\Pomodoro.exe