```markdown
# Simulador de Sistemas de Segunda Ordem

Este projeto implementa um simulador digital para sistemas dinâmicos de segunda ordem utilizando C# (.NET 8.0). A ferramenta permite configurar parâmetros físicos e observar a resposta temporal do sistema discretizado.

## ⚙️ Funcionamento e Discretização

Para a conversão do sistema do domínio contínuo (Plano S) para o domínio discreto (Plano Z), foi utilizada a **Transformada Bilinear (Método de Tustin)**. Esse método é superior à aproximação de Euler por preservar a estabilidade do sistema original e mapear com maior fidelidade a resposta em frequência.

A substituição de Tustin aplicada foi:
$$s \approx \frac{2}{T_s} \frac{1 - z^{-1}}{1 + z^{-1}}$$

O algoritmo resolve a seguinte equação de diferenças recursiva a cada passo de tempo:
`y[k] = b0*u[k] + b1*u[k-1] + b2*u[k-2] - a1*y[k-1] - a2*y[k-2]`

## 🚀 Como usar

1. Com o SDK do .NET instalado, execute no terminal:
   ```bash
   dotnet run
   ```
2. O programa solicitará os seguintes parâmetros:
   * **Frequência Natural (wn):** Define a velocidade de oscilação/resposta.
   * **Razão de Amortecimento (ζ):** Define o comportamento transitório.
   * **Tempo de Amostragem (Ts):** O passo de integração da simulação.

> **Nota:** O software utiliza `CultureInfo.InvariantCulture`. Utilize **ponto (.)** como separador decimal para evitar erros de leitura.

## 📊 Casos de Teste e Comportamentos

| Tipo de Resposta | Valor de ζ | Comportamento Esperado |
| :--- | :--- | :--- |
| **Subamortecido** | 0 < ζ < 1 | Presença de overshoot e oscilações amortecidas. |
| **Criticamente Amortecido** | ζ = 1 | Resposta mais rápida possível sem apresentar oscilação. |
| **Superamortecido** | ζ > 1 | Resposta lenta e gradual, sem ultrapassar o setpoint. |
| **Instável** | ζ < 0 | A saída diverge exponencialmente (simulação de falha). |

## 🛠️ Tecnologias e Conceitos
* **Linguagem:** C# / .NET 8
* **Globalização:** Tratamento de `InvariantCulture` para portabilidade entre regiões.
* **Controle Digital:** Implementação de Equação de Diferenças e Filtros Digitais.

---
**Projeto desenvolvido como aplicação prática de Engenharia de Controle e Automação.**
```