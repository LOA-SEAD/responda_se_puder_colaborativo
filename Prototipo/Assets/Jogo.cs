// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// using UnityEngine.UI;
// using System;
// using UnityEngine.SceneManagement;
// using TMPro;
// using UnityEngine.Networking;


// public class Jogo : MonoBehaviour, IClient
// {

//     private Rect janelaDica = new Rect (0, 0, Screen.width, Screen.height);
//     private bool showJanelaDica = false;

//     public TextMeshProUGUI pergunta_tela;
//     public TextMeshProUGUI alternativa1_tela;
//     public TextMeshProUGUI alternativa2_tela;
//     public TextMeshProUGUI alternativa3_tela;
//     public TextMeshProUGUI alternativa4_tela;
//     public TextMeshProUGUI pontuacao_tela;
//     public TextMeshProUGUI dificuldade_tela;
//     public TextMeshProUGUI numero_questao_tela;
//     public TextMeshProUGUI panel_title;
//     public TextMeshProUGUI panel_TextMeshProUGUI;
//     public Button botao_panel;
//     public Button botao_panel_sim;
//     public Button[] alternativas = new Button[4];
//     public Button botao_pergunta;
//     public Button confirmar;
//     public Button ajuda5050;
//     public Button dica;
//     public Button pular;

//     // public TextMeshProUGUIAsset arquivo_TextMeshProUGUIo;

//     public Animator Panel_anim;
//     public Animator Panel_confirmar_anim;

//     const int SIM = 1;
//     const int NAO = 0;
//     const int FACIL = 0;
//     const int MEDIO = 1;
//     const int DIFICIL = 2;
//     const int INICIO = 0;
//     const int MEIO = 1;
//     const int STATUSOPCOES = 2;
//     const int PROXPALAVRA = 3;
//     const int MUDOU_NIVEL = 1;
//     const int NAO_MUDOU_NIVEL = 0;

//     const int JANELA = 1;
//     const int JOGANDO = 0;

//     int quantidade_facil;
//     int quantidade_medio;
//     int quantidade_dificil;
//     string[] perguntas_facil = new string[11];
//     string[] perguntas_medio = new string[11];
//     string[] perguntas_dificil = new string[11];
//     int[] respostas_facil = new int[11];
//     int[] respostas_medio = new int[11];
//     int[] respostas_dificil = new int[11];
//     string[,] respostas_possiveis_facil = new string[11, 4];
//     string[,] respostas_possiveis_medio = new string[11, 4];
//     string[,] respostas_possiveis_dificil = new string[11, 4];
//     string[] dicas_facil = new string[11];
//     string[] dicas_medio = new string[11];
//     string[] dicas_dificil = new string[11];
//     string[] audios_perguntas = new string[33];
//     string[,] audios_alternativas = new string[33, 4];
//     string[] audios_dicas = new string[33];

//     int alternativa_correta;
//     int pontos_ganhos;
//     int alternativa_escolhida;
//     int tirar_1;
//     int tirar_2;
//     int pular_agora;

//     int questao_x_de_y;
//     int selecionou5050 = NAO;
//     int selecionou_pular = NAO;
//     int nivel_atual = FACIL;
//     int pontuacao;

//     public GameObject[] risco = new GameObject[4];

//     int estado;

//     int Selecionado;
    
//     System.Random random = new System.Random();

//     //Utilizados para o double touch
//     bool one_click = false;
//     bool timer_running;
//     float timer_for_double_click;

//     float delay = 5.0f;

//     void Start()
//     {
//         #if UNITY_ANDROID
//             Screen.orientation = ScreenOrientation.LandscapeLeft;
//             //Screen.fullScreen = false;
//         #endif

//         if (Informacoes.GetStatus() == INICIO)
//         {
//             InicializarJogo();
//         }
//         else
//         {
//             PegarInfosSalvas();
//             PegarProximaQuestao();
//             if(Informacoes.GetStatus() == STATUSOPCOES){
//                 questao_x_de_y--;
//             }
//         }
        
//     }

//     void Update()
//     {

//         cm.retrieveMessages(this);

//         if(estado == JOGANDO)
//         {

//         }
//         else if (estado == JANELA)
//         {
//             //Esc + Enter
//             if (Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.KeypadEnter))
//             {
//                 if(showJanelaDica){
//                     showJanelaDica = false;
//                 }
//                 estado = JOGANDO;
//             }
//         }

//         if(one_click){
//             if((Time.time - timer_for_double_click) > delay){
//                 one_click = false;
//             }
//         }
//     }
    
//     private void InicializarJogo()
//     {
//         carregaDados.Load();
//         //Debug.Log("Quantidade itens: " + carregaDados.listaDados.Count);
//         dificuldade_tela.text = "NÍVEL FÁCIL";
//         pular_agora = NAO;
//         Informacoes.SetStatusPular(NAO);
//         pontuacao = 0;
//         SortearPerguntas();
//     }

//     private void SortearPerguntas()
//     {
//         quantidade_facil = 0;
//         quantidade_medio = 0;
//         quantidade_dificil = 0;
//         for(int i = 0; i < carregaDados.listaDados.Count; i++)
//         {
//             if(carregaDados.listaDados[i].nivel == "facil"){
//                 quantidade_facil++;
//                 carregaDados.listaDados[i].audio_pergunta = "pf" + i;
//                 carregaDados.listaDados[i].audio_dica = "df" + i;
//                 for(int j = 0; j < 4; j++){
//                     carregaDados.listaDados[i].audio_alternativas[j] = "rf" + i + "_" + j;
//                 }
//             }
//             else if(carregaDados.listaDados[i].nivel == "medio"){
//                 quantidade_medio++;
//                 carregaDados.listaDados[i].audio_pergunta = "pm" + (i - quantidade_facil);
//                 carregaDados.listaDados[i].audio_dica = "dm" + (i - quantidade_facil);
//                 for(int j = 0; j < 4; j++){
//                     carregaDados.listaDados[i].audio_alternativas[j] = "rm" + (i - quantidade_facil) + "_" + j;
//                 }
//             }
//             else if(carregaDados.listaDados[i].nivel == "dificil"){
//                 quantidade_dificil++;
//                 carregaDados.listaDados[i].audio_pergunta = "pd" + (i - quantidade_facil - quantidade_medio);
//                 carregaDados.listaDados[i].audio_dica = "dd" + (i - quantidade_facil - quantidade_medio);
//                 for(int j = 0; j < 4; j++){
//                     carregaDados.listaDados[i].audio_alternativas[j] = "rd" + (i - quantidade_facil - quantidade_medio) + "_" + j;
//                 }
//             }
//         //Debug.Log(carregaDados.listaDados[i].audio_pergunta);    
//         }
//         MixLista();
//         AtualizarVariaveis();
//         MisturarRespostas();

//         quantidade_facil--;
//         quantidade_medio--;
//         quantidade_dificil--;
//     }

//     private void MixLista()
//     {
//         for(int i = 0; i < quantidade_facil; i++){
//             DadosJogo aux = new DadosJogo();
//             int limite = quantidade_facil - 1;
//             int randomIndex = GerarNumero(i, limite);
//             if(randomIndex != i){
//                 aux = carregaDados.listaDados[i];
//                 carregaDados.listaDados[i] = carregaDados.listaDados[randomIndex];
//                 carregaDados.listaDados[randomIndex] = aux;
//             }
//         }
//         for(int i = quantidade_facil; i < quantidade_facil + quantidade_medio; i++){
//             DadosJogo aux = new DadosJogo();
//             int limite = quantidade_facil + quantidade_medio - 1;
//             int randomIndex = GerarNumero(i, limite);
//             if(randomIndex != i){
//                 aux = carregaDados.listaDados[i];
//                 carregaDados.listaDados[i] = carregaDados.listaDados[randomIndex];
//                 carregaDados.listaDados[randomIndex] = aux;
//             }
//         }
//         for(int i = quantidade_facil + quantidade_medio; i < quantidade_facil + quantidade_medio + quantidade_dificil; i++){
//             DadosJogo aux = new DadosJogo();
//             int limite = quantidade_facil + quantidade_medio + quantidade_dificil - 1;
//             int randomIndex = GerarNumero(i, limite);
//             if(randomIndex != i){
//                 aux = carregaDados.listaDados[i];
//                 carregaDados.listaDados[i] = carregaDados.listaDados[randomIndex];
//                 carregaDados.listaDados[randomIndex] = aux;
//             }
//         }
//     }

//     private void AtualizarVariaveis()
//     {
//         for(int i = 0; i < quantidade_facil; i++){
//             perguntas_facil[i] = carregaDados.listaDados[i].pergunta;
//             respostas_facil[i] = 0;
//             dicas_facil[i] = carregaDados.listaDados[i].dica;
//             respostas_possiveis_facil[i, 0] = carregaDados.listaDados[i].resposta;
//             respostas_possiveis_facil[i, 1] = carregaDados.listaDados[i].r2;
//             respostas_possiveis_facil[i, 2] = carregaDados.listaDados[i].r3;
//             respostas_possiveis_facil[i, 3] = carregaDados.listaDados[i].r4;
//         }
//         for(int i = 0; i < quantidade_medio; i++){
//             perguntas_medio[i] = carregaDados.listaDados[i+quantidade_facil].pergunta;
//             respostas_medio[i] = 0;
//             dicas_medio[i] = carregaDados.listaDados[i+quantidade_facil].dica;
//             respostas_possiveis_medio[i, 0] = carregaDados.listaDados[i+quantidade_facil].resposta;
//             respostas_possiveis_medio[i, 1] = carregaDados.listaDados[i+quantidade_facil].r2;
//             respostas_possiveis_medio[i, 2] = carregaDados.listaDados[i+quantidade_facil].r3;
//             respostas_possiveis_medio[i, 3] = carregaDados.listaDados[i+quantidade_facil].r4;
//         }
//         for(int i = 0; i < quantidade_dificil; i++){
//             perguntas_dificil[i] = carregaDados.listaDados[i+quantidade_facil+quantidade_medio].pergunta;
//             respostas_dificil[i] = 0;
//             dicas_dificil[i] = carregaDados.listaDados[i+quantidade_facil+quantidade_medio].dica;
//             respostas_possiveis_dificil[i, 0] = carregaDados.listaDados[i+quantidade_facil+quantidade_medio].resposta;
//             respostas_possiveis_dificil[i, 1] = carregaDados.listaDados[i+quantidade_facil+quantidade_medio].r2;
//             respostas_possiveis_dificil[i, 2] = carregaDados.listaDados[i+quantidade_facil+quantidade_medio].r3;
//             respostas_possiveis_dificil[i, 3] = carregaDados.listaDados[i+quantidade_facil+quantidade_medio].r4;
//         }

//         //FACIL
//         for(int i=0; i<11; i++)
//         {
//             if((carregaDados.listaDados[i].nivel == "facil") && (i < carregaDados.listaDados.Count)){
//                 audios_perguntas[i] = carregaDados.listaDados[i].audio_pergunta;
//                 audios_dicas[i] = carregaDados.listaDados[i].audio_dica;
//                 for(int j = 0; j < 4; j++)
//                 {
//                     audios_alternativas[i, j] = carregaDados.listaDados[i].audio_alternativas[j];
//                 }
//             }
//         }
//         //MEDIO
//         int aux_medio = 0;
//         for(int i=11; i<22; i++)
//         {
//             if(i-11 <= carregaDados.listaDados.Count && aux_medio < quantidade_medio){
//                 audios_perguntas[i] = carregaDados.listaDados[i-11+quantidade_facil].audio_pergunta;
//                 audios_dicas[i] = carregaDados.listaDados[i-11+quantidade_facil].audio_dica;
//                 for(int j = 0; j < 4; j++)
//                 {
//                     audios_alternativas[i, j] = carregaDados.listaDados[i-11+quantidade_facil].audio_alternativas[j];
//                 }
//                 aux_medio++;
//             }
//         }
//         //DIFICIL
//         int aux_dificil = 0;
//         for(int i=22; i<33; i++)
//         {
//             if(i-22 <= carregaDados.listaDados.Count && aux_dificil < quantidade_dificil){
//                 audios_perguntas[i] = carregaDados.listaDados[i-22+quantidade_facil+quantidade_medio].audio_pergunta;
//                 audios_dicas[i] = carregaDados.listaDados[i-22+quantidade_facil+quantidade_medio].audio_dica;
//                 for(int j = 0; j < 4; j++)
//                 {
//                     audios_alternativas[i, j] = carregaDados.listaDados[i-22+quantidade_facil+quantidade_medio].audio_alternativas[j];
//                 }
//                 aux_dificil++;
//             }
//         }
//     }

//     private void MisturarRespostas()
//     {
//         for (int i = 0; i < quantidade_facil + 1; i++)
//         {
//             string aux_s;
//             string aux_audio;
//             int troca1 = GerarNumero(0, 4);
//             int troca2 = GerarNumero(0, 4);
//             if (respostas_facil[i] == troca1)
//             {
//                 respostas_facil[i] = troca2;
//             }
//             else if (respostas_facil[i] == troca2)
//             {
//                 respostas_facil[i] = troca1;
//             }
//             aux_s = respostas_possiveis_facil[i, troca1];
//             respostas_possiveis_facil[i, troca1] = respostas_possiveis_facil[i, troca2];
//             respostas_possiveis_facil[i, troca2] = aux_s;

//             aux_audio = audios_alternativas[i, troca1];
//             audios_alternativas[i, troca1] = audios_alternativas[i, troca2];
//             audios_alternativas[i, troca2] = aux_audio;
//         }

//         for (int i = 0; i < quantidade_medio + 1; i++)
//         {
//             string aux_s;
//             string aux_audio;
//             int troca1 = GerarNumero(0, 4);
//             int troca2 = GerarNumero(0, 4);
//             if (respostas_medio[i] == troca1)
//             {
//                 respostas_medio[i] = troca2;
//             }
//             else if (respostas_medio[i] == troca2)
//             {
//                 respostas_medio[i] = troca1;
//             }
//             aux_s = respostas_possiveis_medio[i, troca1];
//             respostas_possiveis_medio[i, troca1] = respostas_possiveis_medio[i, troca2];
//             respostas_possiveis_medio[i, troca2] = aux_s;

//             aux_audio = audios_alternativas[i+11, troca1];
//             audios_alternativas[i+11, troca1] = audios_alternativas[i+11, troca2];
//             audios_alternativas[i+11, troca2] = aux_audio;
//         }

//         for (int i = 0; i < quantidade_dificil + 1; i++)
//         {
//             string aux_s;
//             string aux_audio;
//             int troca1 = GerarNumero(0, 4);
//             int troca2 = GerarNumero(0, 4);
//             if (respostas_dificil[i] == troca1)
//             {
//                 respostas_dificil[i] = troca2;
//             }
//             else if (respostas_dificil[i] == troca2)
//             {
//                 respostas_dificil[i] = troca1;
//             }
//             aux_s = respostas_possiveis_dificil[i, troca1];
//             respostas_possiveis_dificil[i, troca1] = respostas_possiveis_dificil[i, troca2];
//             respostas_possiveis_dificil[i, troca2] = aux_s;

//             aux_audio = audios_alternativas[i+22, troca1];
//             audios_alternativas[i+22, troca1] = audios_alternativas[i+22, troca2];
//             audios_alternativas[i+22, troca2] = aux_audio;
//         }
//     }

//     private void AtualizarPerguntaTela()
//     {
//         estado = JOGANDO;
//         botao_pergunta.Select();
//         ExibirNaTela();
//         ConfigurarBotoes();
//         #if UNITY_ANDROID
//         #else
//         #endif
//     }

//     public void LimparDados()
//     {
        
//     }

//     private void SalvarInfos()
//     {
//         Informacoes.SetQuantidadeFacil(quantidade_facil);
//         Informacoes.SetQuantidadeMedio(quantidade_medio);
//         Informacoes.SetQuantidadeDificil(quantidade_dificil);
//         Informacoes.SetNivel(nivel_atual);
//         Informacoes.SetPontos(pontuacao);
//         Informacoes.SetPerguntasFacil(perguntas_facil);
//         Informacoes.SetRespostasFacil(respostas_facil);
//         Informacoes.SetRespostasPossiveisFacil(respostas_possiveis_facil);
//         Informacoes.SetDicasFacil(dicas_facil);
//         Informacoes.SetPerguntasMedio(perguntas_medio);
//         Informacoes.SetRespostasMedio(respostas_medio);
//         Informacoes.SetRespostasPossiveisMedio(respostas_possiveis_medio);
//         Informacoes.SetDicasMedio(dicas_medio);
//         Informacoes.SetPerguntasDificil(perguntas_dificil);
//         Informacoes.SetRespostasDificil(respostas_dificil);
//         Informacoes.SetRespostasPossiveisDificil(respostas_possiveis_dificil);
//         Informacoes.SetDicasDificil(dicas_dificil);
//         Informacoes.SetStatus5050(selecionou5050);

//         Informacoes.SetAudiosPerguntas(audios_perguntas);
//         Informacoes.SetAudiosAlternativas(audios_alternativas);
//         Informacoes.SetAudiosDicas(audios_dicas);

//         Informacoes.SetNumeroQuestao(questao_x_de_y);
//     }
    
//     private void PegarInfosSalvas()
//     {
//         if(Informacoes.GetStatus() == STATUSOPCOES || Informacoes.GetStatus() == PROXPALAVRA){
//             questao_x_de_y = Informacoes.GetNumeroQuestao();
//         }
//         else{
//             questao_x_de_y = -1;
//         }
//         pular_agora = NAO;
//         quantidade_facil = Informacoes.GetQuantidadeFacil();
//         quantidade_medio = Informacoes.GetQuantidadeMedio();
//         quantidade_dificil = Informacoes.GetQuantidadeDificil();
//         nivel_atual = Informacoes.GetNivel();
//         pontuacao = Informacoes.GetPontos();
//         perguntas_facil = Informacoes.GetPerguntasFacil();
//         respostas_facil = Informacoes.GetRespostasFacil();
//         respostas_possiveis_facil = Informacoes.GetRespostasPossiveisFacil();
//         dicas_facil = Informacoes.GetDicasFacil();
//         perguntas_medio = Informacoes.GetPerguntasMedio();
//         respostas_medio = Informacoes.GetRespostasMedio();
//         respostas_possiveis_medio = Informacoes.GetRespostasPossiveisMedio();
//         dicas_medio = Informacoes.GetDicasMedio();
//         perguntas_dificil = Informacoes.GetPerguntasDificil();
//         respostas_dificil = Informacoes.GetRespostasDificil();
//         respostas_possiveis_dificil = Informacoes.GetRespostasPossiveisDificil();
//         dicas_dificil = Informacoes.GetDicasDificil();
//         selecionou5050 = Informacoes.GetStatus5050();
//         selecionou_pular = Informacoes.GetStatusPular();

//         audios_perguntas = Informacoes.GetAudiosPerguntas();
//         audios_alternativas = Informacoes.GetAudiosAlternativas();
//         audios_dicas = Informacoes.GetAudiosDicas();
//     }

//     public void TocarHighlight(int alternativa)
//     {
//         #if UNITY_ANDROID
//             pausarAudios();
//             if(alternativa == 0){
//                 if(alternativas[0].interactable){
//                     audio_a0.Play();
//                     alternativas[0].Select();
//                 }
//             }
//             if (alternativa == 1){
//                 if(alternativas[1].interactable){
//                     audio_a1.Play();
//                     alternativas[1].Select();
//                 }
//             }
//             if (alternativa == 2){
//                 if(alternativas[2].interactable){
//                     audio_a2.Play();
//                     alternativas[2].Select();
//                 }
//             }
//             if (alternativa == 3){
//                 if(alternativas[3].interactable){
//                     audio_a3.Play();
//                     alternativas[3].Select();
//                 }
//             }
//             //Pergunta
//             if (alternativa == 4){
//                 audio_pergunta.Play();
//                 botao_pergunta.Select();
//             }
//             //Dica
//             if(alternativa == 5){
//                 dica.Select();
//             }
//             //5050
//             if(alternativa == 6){
//                 if(ajuda5050.interactable){
//                     ajuda5050.Select();
//                 }
//             }
//             //Pular
//             if(alternativa == 7){
//                 if(pular.interactable){
//                     pular.Select();
//                 }
//             }
//         #endif
//     }


//     public void ObjetoSelecionado(int FlagSelecionado){
//         Selecionado = FlagSelecionado;
//     }

//     public void EscolherAlternativa(string alternativa)
//     {
//         alternativa_escolhida = Convert.ToInt32(alternativa);
//         #if UNITY_ANDROID
//             if(!one_click){
//                 one_click = true;
//                 timer_for_double_click = Time.time;
                
//                 if(alternativa_escolhida == 0){
//                     audio_a0.Play();
//                 }
//                 else if (alternativa_escolhida == 1){
//                     audio_a1.Play();
//                 }
//                 else if (alternativa_escolhida == 2){
//                     audio_a2.Play();
//                 }
//                 else if (alternativa_escolhida == 3){
//                     audio_a3.Play();
//                 }
//                 else if (alternativa_escolhida == 4)
//                     audio_pergunta.Play();
//             }
//             else{
//                 one_click = false;

//                 alternativas[alternativa_escolhida].Select();
//                 confirmar.interactable = true;
//                 confirmar.interactable = false;
//                 MostrarPanelConfirmar();
//             }
//         #else
//             alternativa_escolhida = Convert.ToInt32(alternativa);
//             alternativas[alternativa_escolhida].Select();
//             confirmar.interactable = true;

//             confirmar.interactable = false;

//             MostrarPanelConfirmar();
//         #endif
//     }

//     public void MostrarPanelConfirmar(){
//         Panel_confirmar_anim.SetBool("showPanel", true);
//         botao_panel_sim.Select();
//     }

//     public void EsconderPanelConfirmar(){
//         Panel_confirmar_anim.SetBool("showPanel", false);
//     }
    
//     public void ConfirmarAlternativa()
//     {
//         confirmar.interactable = false;
//         SomarPontuacao();
//         EsconderPanelConfirmar();
//     }

//     public void NaoConfirmarAlternativa(){
//         EsconderPanelConfirmar();
//         alternativas[alternativa_escolhida].Select();
//     }

//     public void Funcao5050()
//     {
//         #if UNITY_ANDROID
//             if(!one_click){
//                 one_click = true;
//                 timer_for_double_click = Time.time;

//                 pausarAudios();
//                 audio_5050.Play();
//             }
//             else{
//                 one_click = false;

//                 selecionou5050 = SIM;
//                 confirmar.interactable = false;

//                 tirar_1 = GerarNumero(0, 4);
//                 while (tirar_1 == alternativa_correta)
//                 {
//                     tirar_1 = GerarNumero(0, 4);
//                 }
//                 tirar_2 = GerarNumero(0, 4);
//                 while (tirar_1 == tirar_2 || tirar_2 == alternativa_correta)
//                 {
//                     tirar_2 = GerarNumero(0, 4);
//                 }
//                 alternativas[tirar_1].interactable = false;
//                 alternativas[tirar_2].interactable = false;

//                 risco[tirar_1].SetActive(true);
//                 risco[tirar_2].SetActive(true);

//                 ajuda5050.interactable = false;
//                 for(int i=0; i<4; i++){
//                     if(alternativas[i].interactable)
//                     {
//                         alternativas[i].Select();
//                         break;
//                     }
//                 }
//             }
//         #else
//             selecionou5050 = SIM;
//             confirmar.interactable = false;

//             tirar_1 = GerarNumero(0, 4);
//             while (tirar_1 == alternativa_correta)
//             {
//                 tirar_1 = GerarNumero(0, 4);
//             }
//             tirar_2 = GerarNumero(0, 4);
//             while (tirar_1 == tirar_2 || tirar_2 == alternativa_correta)
//             {
//                 tirar_2 = GerarNumero(0, 4);
//             }
//             alternativas[tirar_1].interactable = false;
//             alternativas[tirar_2].interactable = false;

//             risco[tirar_1].SetActive(true);
//             risco[tirar_2].SetActive(true);

//             ajuda5050.interactable = false;
//             for(int i=0; i<4; i++){
//                 if(alternativas[i].interactable)
//                 {
//                     alternativas[i].Select();
//                     break;
//                 }
//             }
//         #endif
//     }

//     public void FuncaoDica()
//     {
//         #if UNITY_ANDROID
//             if(!one_click){
//                 one_click = true;
//                 timer_for_double_click = Time.time;

//                 pausarAudios();
//                 audio_botao_dica.Play();
//             }
//             else{
//                 one_click = false;

//                 confirmar.interactable = false;
//                 estado = JANELA;
//                 audio_dica.Play();
//                 botao_panel.Select();
//                 CriarJanelaDica();
//             }
//         #else
//             confirmar.interactable = false;
//             estado = JANELA;
//             botao_panel.Select();
//             CriarJanelaDica();
//         #endif
//     }

//     public void FuncaoPular()
//     {
//         #if UNITY_ANDROID
//             if(!one_click){
//                 one_click = true;
//                 timer_for_double_click = Time.time;

//             }
//             else{
//                 one_click = false;

//                 Informacoes.SetStatusPular(SIM);
//                 selecionou_pular = SIM;
//                 confirmar.interactable = false;
//                 pular.interactable = false;
//                 pular_agora = SIM;
//                 PegarProximaQuestao();
//                 AtualizarPerguntaTela();
//                 alternativas[0].Select();
//             }
//         #else
//             Informacoes.SetStatusPular(SIM);
//             selecionou_pular = SIM;
//             confirmar.interactable = false;
//             pular.interactable = false;
//             pular_agora = SIM;
//             PegarProximaQuestao();
//             AtualizarPerguntaTela();
//             alternativas[0].Select();
//         #endif
//     }

//     private int GerarNumero(int min, int max)
//     {
//         return random.Next(min, max);
//     }

//     private void SomarPontuacao()
//     {
//         if (nivel_atual == FACIL)
//         {
//             if(alternativa_escolhida == respostas_facil[questao_x_de_y])
//             {
//                 pontos_ganhos = 10;
//             }
//             else
//             {
//                 pontos_ganhos = 0;
//             }
//         }
//         else if (nivel_atual == MEDIO)
//         {
//             if(alternativa_escolhida == respostas_medio[questao_x_de_y])
//             {
//                 pontos_ganhos = 10;
//             }
//             else
//             {
//                 pontos_ganhos = 0;
//             }
//         }
//         else
//         {
//             if(alternativa_escolhida == respostas_dificil[questao_x_de_y])
//             {
//                 pontos_ganhos = 10;
//             }
//             else
//             {
//                 pontos_ganhos = 0;
//             }
//         }
        
//         pontuacao += pontos_ganhos;
//     }

//     public int AtualizarNivel()
//     {
//         PegarInfosSalvas();
//         //questao_x_de_y ++;
//         Debug.Log(questao_x_de_y);
//         Debug.Log(quantidade_facil);
//         if (pular_agora == NAO)
//         {
//             if (nivel_atual == FACIL && questao_x_de_y >= quantidade_facil-1)
//             {
//                 Informacoes.SetStatus(MEIO);
//                 nivel_atual = MEDIO;
//                 SalvarInfos();
//                 return MUDOU_NIVEL;
//             }
//             else if (nivel_atual == MEDIO && questao_x_de_y >= quantidade_medio-1)
//             {
//                 Informacoes.SetStatus(MEIO);
//                 nivel_atual = DIFICIL;
//                 SalvarInfos();
//                 return MUDOU_NIVEL;
//             }
//             SalvarInfos();
//             return NAO_MUDOU_NIVEL;
//         }
//         else
//         {
//             if (nivel_atual == FACIL && questao_x_de_y >= quantidade_facil)
//             {
//                 Informacoes.SetStatus(MEIO);
//                 nivel_atual = MEDIO;
//                 SalvarInfos();
//                 return MUDOU_NIVEL;
//             }
//             else if (nivel_atual == MEDIO && questao_x_de_y >= quantidade_medio)
//             {
//                 Informacoes.SetStatus(MEIO);
//                 nivel_atual = DIFICIL;
//                 SalvarInfos();
//                 return MUDOU_NIVEL;
//             }
//             SalvarInfos();
//             return NAO_MUDOU_NIVEL;
//         }
        
//     }

//     private void ExibirNaTela()
//     {
//         for(int i = 0; i < 4; i++)
//             risco[i].SetActive(false);

//         if (nivel_atual == FACIL)
//         {
//             ExibirNaTelaFacil();
//         }
//         else if (nivel_atual == MEDIO)
//         {
//             ExibirNaTelaMedio();
//         }
//         else
//         {
//             ExibirNaTelaDificil();
//         }
//     }

//     private void ExibirNaTelaFacil()
//     {
//         dificuldade_tela.text = "NÍVEL FÁCIL";
//         if (pular_agora == SIM)
//             numero_questao_tela.text = "Questão " + (questao_x_de_y).ToString() + " de " + quantidade_facil.ToString();
//         else
//             numero_questao_tela.text = "Questão " + (questao_x_de_y + 1).ToString() + " de " + quantidade_facil.ToString();
//         pergunta_tela.text = perguntas_facil[questao_x_de_y];
//         alternativa_correta = respostas_facil[questao_x_de_y];
        
//         alternativa1_tela.text = respostas_possiveis_facil[questao_x_de_y, 0];
//         alternativa2_tela.text = respostas_possiveis_facil[questao_x_de_y, 1];
//         alternativa3_tela.text = respostas_possiveis_facil[questao_x_de_y, 2];
//         alternativa4_tela.text = respostas_possiveis_facil[questao_x_de_y, 3];

//         pontuacao_tela.text = "Pontos: " + pontuacao.ToString();
//     }

//     private void ExibirNaTelaMedio()
//     {
//         dificuldade_tela.text = "NÍVEL MÉDIO";
//         if (pular_agora == SIM)
//             numero_questao_tela.text = "Questão " + (questao_x_de_y).ToString() + " de " + quantidade_medio.ToString();
//         else
//             numero_questao_tela.text = "Questão " + (questao_x_de_y + 1).ToString() + " de " + quantidade_medio.ToString();
//         pergunta_tela.text = perguntas_medio[questao_x_de_y];
//         alternativa_correta = respostas_medio[questao_x_de_y];
        
//         alternativa1_tela.text = respostas_possiveis_medio[questao_x_de_y, 0];
//         alternativa2_tela.text = respostas_possiveis_medio[questao_x_de_y, 1];
//         alternativa3_tela.text = respostas_possiveis_medio[questao_x_de_y, 2];
//         alternativa4_tela.text = respostas_possiveis_medio[questao_x_de_y, 3];

//         pontuacao_tela.text = "Pontos: " + pontuacao.ToString();
//     }

//     private void ExibirNaTelaDificil()
//     {
//         dificuldade_tela.text = "NÍVEL DIFÍCIL";
//         if (pular_agora == SIM)
//             numero_questao_tela.text = "Questão " + (questao_x_de_y).ToString() + " de " + quantidade_dificil.ToString();
//         else
//             numero_questao_tela.text = "Questão " + (questao_x_de_y + 1).ToString() + " de " + quantidade_dificil.ToString();
//         pergunta_tela.text = perguntas_dificil[questao_x_de_y];
//         alternativa_correta = respostas_dificil[questao_x_de_y];
            
//         alternativa1_tela.text = respostas_possiveis_dificil[questao_x_de_y, 0];
//         alternativa2_tela.text = respostas_possiveis_dificil[questao_x_de_y, 1];
//         alternativa3_tela.text = respostas_possiveis_dificil[questao_x_de_y, 2];
//         alternativa4_tela.text = respostas_possiveis_dificil[questao_x_de_y, 3];

//         pontuacao_tela.text = "Pontos: " + pontuacao.ToString();
//     }

//     private void PegarProximaQuestao()
//     {
//         questao_x_de_y++;
//     }

//     private void ConfigurarBotoes()
//     {
//         for (int i = 0; i < 4; i++)
//         {
//             alternativas[i].interactable = true;
//         }
//         confirmar.interactable = false;
//         if (selecionou5050 == SIM)
//         {
//             ajuda5050.interactable = false;
//         }
//         if (selecionou_pular == SIM)
//         {
//             pular.interactable = false;
//         }
//     }

//     private GUIStyle guiStyle = new GUIStyle();

//     private void OnGUI () 
//     {
        
//     }

//     private void CriarJanelaDica ()
//     {
//         Panel_anim.SetBool("showPanel", true);
//         panel_title.text = "DICA";
//         if (nivel_atual == FACIL)
//         {
//             panel_TextMeshProUGUI.text = dicas_facil[questao_x_de_y];
//         }
//         else if (nivel_atual == MEDIO)
//         {
//             panel_TextMeshProUGUI.text = dicas_medio[questao_x_de_y];
//         }
//         else
//         {
//             panel_TextMeshProUGUI.text = dicas_dificil[questao_x_de_y];
//         }
//     }

//     public void EsconderPanel()
//     {
//         dica.Select();

//         Panel_anim.SetBool("showPanel", false);
//         estado = JOGANDO;
//     }
// }



using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;


public class Jogo : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    public TMP_Text pergunta;
    public TMP_Text dica;
    public TMP_Text[] alternativas; 
    public Button[] btnAlternativas;
    public TMP_Text altA;
    public TMP_Text altB;
    public TMP_Text altC;
    public TMP_Text altD;

    public TMP_Text txt_correto_resposta;
    public TMP_Text txt_errado_resposta;
    public TMP_Text txt_errado_resposta_dada;
    
    public TMP_Text numeroQuestaoText;
    public TMP_Text nivel;
    public TMP_Text pontuacao; 

    public TMP_Text tempoQuestao; 
    public float timer = 20f;

    public GameObject CanvasJogo;
    public GameObject CanvasRCerta;
    public GameObject CanvasRErrada;
    public GameObject CanvasFase;


    public GameObject[] qntAlternatives;

    public GameObject confirmaAlternativa;
    public GameObject painelDica;
    public GameObject painelConfirma;
    public GameObject quadroChat;

    public Button btnDica;
    public Button confirmaDica;

    public int[] alt;
    private DadosJogo perguntaAtual;
    private int numeroQuestao = 0;
    private int totalQuestoes;

    private string correctAnswer;
    public ans answer;
    private int level;
    private int nrQuestion;
    private RespostasGrupo ansGroup;
    private int correct;

    private int qst;


    //TELA FASE
    public float sec;
    public TMP_Text txt_nivel;
    public TMP_Text txt_lider; 

    public TMP_Text txt_nrEquipe;

    // private void Start()
    // {
    //     SetLevelText();
    //     SetLeaderText();
    //     Invoke("NextQ", sec);
    // }


    void NextQ()
    {
        CanvasJogo.SetActive(true);
        CanvasFase.SetActive(false);
    }

    public void SetLevelText()
    {
        txt_nivel.text = Manager.FASE;
    }

    public void SetLeaderText()
    {
        string t = dadosTimes.GetUser(Manager.leaderId);

        txt_lider.text = "Líder da fase: " + t;
    }


    // Start is called before the first frame update
    void Start()
    {
        // tempoQuestao.text = timer.ToString();
        tempoQuestao.text = "Tempo Restante: " + timer;
        pontuacao.text = "Pontuação: ";
        pontuacao.text = pontuacao.text + "0";

        // txt_nrEquipe.text = "Equipe " + Manager.teamId;
        SetQntAlternatives(0);


        painelDica.gameObject.SetActive(false);
        painelConfirma.gameObject.SetActive(false);

        Manager.totalQuestoes = 0;
        Manager.totalFacil = 0;
        Manager.totalMedio = 0;
        Manager.totalDificil = 0;
        qst = 0;


        carregaDados.Load();
        carregaDados.Select();
        totalQuestoes = carregaDados.listaDados.Count;

        Manager.countQuestoesJogo();

        CanvasJogo.SetActive(false);
        CanvasFase.SetActive(true);
        SetLevelText();
        SetLeaderText();
        Invoke("NextQ", sec);

        PrimeiraQuestao();


        
        
        // CarregarPerguntas();
        
        // generalCommands.DisableAllObjectsInteractions();
        // btnDica.interactable = true;
        // quadroChat.SetActive(true);
        // generalCommands.EnableInteraction(quadroChat);
        // Debug.Log(Manager.totalQuestoes);
        // Debug.Log(totalQuestoes);
    }

    void PrimeiraQuestao()
    {
        MSG_NOVA_QUESTAO(Manager.msgPrimeiraQuestao);
    }
    // void CarregarPerguntas()
    // {
    //     // numeroQuestao = 1;
    //     CarregarPergunta();
    //     // SetIndividual();
    //     // ProximaQuestao();
    // }

    // Carrega uma nova pergunta
    void CarregarPergunta()
    {
        if (carregaDados.listaDados.Count > 0)
        {
            perguntaAtual = carregaDados.listaDados[0];
            carregaDados.listaDados.RemoveAt(0);
        }
        else
        {
            perguntaAtual = null;
            //fim de jogo
        }

        // Placar de questões e tempo
        numeroQuestaoText.text = "Questão " + numeroQuestao + " de " + Manager.nQ_total;
        nivel.text = "Nível " + perguntaAtual.nivel;
        
        if (perguntaAtual.nivel == "facil")
        {
            answer.level = 0;
        }
        else if (perguntaAtual.nivel == "medio")
        {
            answer.level = 1;
        }
        else
        {
            answer.level = 2;
        }


        // Printa as perguntas e respostas
        if (perguntaAtual != null)
        {
            correctAnswer = perguntaAtual.resposta; 
            pergunta.text = perguntaAtual.pergunta;
            dica.text = perguntaAtual.dica;
            answer.nrQ = Manager.numQ[qst];


            carregaDados.Shuffle(ref perguntaAtual, alt);
            
            // string[] alt_string = {perguntaAtual.resposta, perguntaAtual.r2, perguntaAtual.r3, perguntaAtual.r4};
            // List<string> novaOrdem = new List<string>();

            // foreach (int i in alt)
            // {
            //     if (i >= 0 && i < alt_string.Length)
            //     {
            //         novaOrdem.Add(alt_string[i]);
            //     }
            // }

            // if (novaOrdem.Count == alt_string.Length)
            // {
            //     perguntaAtual.resposta = novaOrdem[0];
            //     perguntaAtual.r2 = novaOrdem[1];
            //     perguntaAtual.r3 = novaOrdem[2];
            //     perguntaAtual.r4 = novaOrdem[3];
            // }

            Debug.Log(perguntaAtual.resposta);
            Debug.Log(perguntaAtual.r2);
            Debug.Log(perguntaAtual.r3);
            Debug.Log(perguntaAtual.r4);


            for (int i = 0; i < btnAlternativas.Length; i++)
            {  
                
                alternativas[i].text = ObterAlternativa(i);
                btnAlternativas[i].gameObject.SetActive(true);
            }

            confirmaAlternativa.SetActive(false);
        }
        else
        {

            for (int i = 0; i < btnAlternativas.Length; i++)
            {
                btnAlternativas[i].gameObject.SetActive(false);
            }
        }
    }


    

    public void SelecionaAlternativa(string tag)
    {
  
        switch (tag)
        {   
            case "A":
                Debug.Log(perguntaAtual.resposta);
                answer.s = perguntaAtual.resposta;
                answer.alternativa = "A";
                break;
            case "B":
                Debug.Log(perguntaAtual.r2);
                answer.s = perguntaAtual.r2;
                answer.alternativa = "B";
                break;
            case "C":
                Debug.Log(perguntaAtual.r3);
                answer.s = perguntaAtual.r3;
                answer.alternativa = "C";
                break;
            case "D":
                answer.s = perguntaAtual.r4;
                answer.alternativa = "D";
                Debug.Log(perguntaAtual.r4);
                break;
            default:
                Debug.Log("Nada selecionado");
                break;
        }

        painelConfirma.SetActive(true);


        answer.alternativa = tag;

    }

    string ObterAlternativa(int index)
    {
        switch (index)
        {
            case 0:
                return perguntaAtual.resposta;
            case 1:
                return perguntaAtual.r2;
            case 2:
                return perguntaAtual.r3;
            case 3:
                return perguntaAtual.r4;
            default:
                return "";
        }
    }

    public void ProximaQuestao()
    {
        numeroQuestao++;

        if (numeroQuestao <= Manager.nQ_total)
        {
            CarregarPergunta();
        }
        else {
            SceneManager.LoadScene("Fim");
        }
        zeraTimer();

    }

    public int VerificaResposta()
    {
        if (correctAnswer == answer.s) return 1;
        else return 0;

    }

    public void ConfirmarResposta()
    {
        Debug.Log("RESPOSTA: "+ answer.alternativa);
        Debug.Log(Manager.MOMENTO);
        
        if (Manager.MOMENTO == "INDIVIDUAL")
        {
            ConfirmarRespostaIndividual();
        }
        if (Manager.MOMENTO == "GRUPO")
        {
            ConfirmarRespostaFinal();
        }

        // painelConfirma.SetActive(false);
        // //ProximaQuestao();
    }

    public void ConfirmarRespostaIndividual()
    {
        painelConfirma.SetActive(false);


        correct = VerificaResposta();
        
        if (correct == 1) {
            Manager.indScore += 10;
        }

        var msg = new RespostaIndividual("RESPOSTA_INDIVIDUAL", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                        Manager.gameId, answer.alternativa, answer.level, answer.nrQ);

        cm.send(msg);

    }

    public void ConfirmarRespostaFinal()
    {
        painelConfirma.SetActive(false);

        correct = VerificaResposta();

        var msg = new RespostaFinal("RESPOSTA_FINAL", dadosTimes.player, Manager.teamId, Manager.sessionId, 
                                    Manager.gameId, answer.alternativa, correct);

        cm.send(msg);

        // qst++;

     }

    public void ajudaDica()
    {
        painelDica.SetActive(true);
    }

    public void confirmarDica()
    {
        painelDica.SetActive(false);
    }



    public void ajudaPula()
    {
        if (Manager.MOMENTO == "GRUPO"){
            var msg = new PedirAjuda("PEDIR_AJUDA", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                    Manager.gameId, "pular");

            cm.send(msg);
        }
    }

    public void ajuda5050()
    {
        if (Manager.MOMENTO == "GRUPO"){
            var msg = new PedirAjuda("PEDIR_AJUDA", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                    Manager.gameId, "5050");

            cm.send(msg);
        }
    }



    void Update()
    {
        atualizaTimer();

        cm.retrieveMessages(this);


    }

    public void SetQntAlternatives(int i)
    {
        if (i == 0)
        {
            foreach (GameObject obj in qntAlternatives)
            {
                obj.SetActive(false);
            }
        }
        else 
        {
            foreach (GameObject obj in qntAlternatives)
            {
                obj.SetActive(true);
            } 
        }
    }

    public void atualizaTimer()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
        //    if (Manager.MOMENTO = "INDIVIDUAL") 
        //    {

        //    }
        //    else 
        //    {

        //    }
        }

        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);


        string timeString = string.Format("{0:00}:{1:00}", min, sec);

        tempoQuestao.text = "Tempo Restante: " + timeString;
    }

    public void zeraTimer()
    {

        timer = Manager.time;
        // timer = 100f;
    }

    public void SetIndividual()
    {
        Manager.MOMENTO = "INDIVIDUAL";

        generalCommands.EnableAllObjectsInteractions();
        
        SetQntAlternatives(0);

        quadroChat.SetActive(false);
    }

    public void SetGrupo()
    {
        zeraTimer();
        Manager.MOMENTO = "GRUPO";
        
        SetQntAlternatives(1);

        quadroChat.SetActive(true);


        Debug.Log("ID JOGADOR: " + dadosTimes.player.id);

        Debug.Log("ID Lider: " + Manager.leaderId);

        if (dadosTimes.player.id != Manager.leaderId)
        {
            
            generalCommands.DisableAllObjectsInteractions();

            btnDica.interactable = true;
            generalCommands.EnableInteraction(quadroChat);
            //configurações

        }
    }

    public void EncerraQuestao(string ans, int correct) 
    {
        
        if (correct == 1) {
            CanvasJogo.SetActive(false);
            CanvasRCerta.SetActive(true);
            txt_correto_resposta.text = "A resposta correta é " + answer.s;
        }
        else {
            CanvasJogo.SetActive(false);
            CanvasRErrada.SetActive(true);
            txt_errado_resposta.text = "Sua equipe respondeu " + answer.s + "\nA resposta correta é " + perguntaAtual.resposta;
            // txt_errado_resposta_dada.text = "A resposta correta é " + perguntaAtual.resposta;
        }






            Invoke("AtivarTelaJogo", 10f);


    }

    public void AtivarTelaJogo() 
    {
        CanvasJogo.SetActive(true);
        CanvasRCerta.SetActive(false);
        CanvasRErrada.SetActive(false);

        SetIndividual();
        ProximaQuestao();
        // var msg = new ProxQuestao("PROXIMA_QUESTAO", dadosTimes.player, Manager.teamId, Manager.sessionId,
        //                             Manager.gameId);

        // cm.send(msg);

    }



    public void     (string msgJSON) 
    {
        msgNOVA_QUESTAO message = JsonUtility.FromJson<msgNOVA_QUESTAO>(msgJSON);
        
        Manager.leaderId = message.leaderId;
        alt = message.alternativas;

        SetIndividual();

        ProximaQuestao();
    }

    public void MSG_MOMENTO_GRUPO(string msgJSON)
    {
        msgMOMENTO_GRUPO message = JsonUtility.FromJson<msgMOMENTO_GRUPO>(msgJSON);



        // Manager.leaderId = message.leaderId;
        altA.text = "" + message.answer.A;
        altB.text = "" + message.answer.B;
        altC.text = "" + message.answer.C;
        altD.text = "" + message.answer.D;
        
        SetGrupo();

    }

    public void MSG_FINAL_QUESTAO(string msgJSON)
    {

        msgFINAL_QUESTAO message = JsonUtility.FromJson<msgFINAL_QUESTAO>(msgJSON);
        
        answer.s = message.finalAnswer;
        correct = message.correct;

        EncerraQuestao(answer.alternativa, correct);

        qst++;


    }

    public void MSG_AJUDA(string msgJSON)
    {
        msgAJUDA_PULAR message = JsonUtility.FromJson<msgAJUDA_PULAR>(msgJSON);
        

        if (message.help == "pular"){
            ProximaQuestao();
        }
        else{
            
        }

    }


    public void handle(string ms)
    {

        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;


        // Debug.Log("CENA JOGO: " + ms);
        // route message to handler based on message type

        if (messageType == "NOVA_QUESTAO") 
        {
            MSG_NOVA_QUESTAO(ms);

        }
        else if (messageType == "MOMENTO_GRUPO")
        {
            MSG_MOMENTO_GRUPO(ms);
        }
        else if (messageType == "FINAL_QUESTAO")
        {
            MSG_FINAL_QUESTAO(ms);
        }
        else if (messageType == "AJUDA_EQUIPE"){
            MSG_AJUDA(ms);
        }
    }

}

[System.Serializable]
public class msgNOVA_QUESTAO
{
    public string message_type;
    public int[] alternativas;
    public int teamId;
    public int leaderId;
    public int sessionId;
    public int gameId;
}

[System.Serializable]
public class msgAJUDA_PULAR
{
    public string message_type;
    public int teamId;
    public int sessionId;
    public int gameId;
    public string help;
}

[System.Serializable]
public class msgAJUDA_5050
{
    public string message_type;
    public int teamId;
    public int sessionId;
    public int gameId;
    public string help;
    public int[] alternativa;

}


[System.Serializable]
public class msgMOMENTO_GRUPO
{
    public string message_type;
    public int teamId;
    public int leaderId;
    public string sessionId;
    public int gameId;
    public RespostasGrupo answer;
}

[System.Serializable]
public class RespostasGrupo
{
    public int A;
    public int B;
    public int C;
    public int D;
}

[System.Serializable]
public class msgFINAL_QUESTAO
{
    public string message_type;
    public int teamId;
    public string sessionId;
    public int gameId;
    public string finalAnswer;
    public int correct;
}

[System.Serializable]
public class ans
{
    public string s;
    public string alternativa;
    public int level;
    public int nrQ;

}