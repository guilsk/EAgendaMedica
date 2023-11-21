import { ListarAtividadeViewModel } from "../../atividades/models/listar-atividade.view-model"

export type VisualizarMedicoViewModel = {
    id: string
    nome: string
    crm: string
    atividades: Array<ListarAtividadeViewModel>
}