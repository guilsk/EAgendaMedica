import { ListarMedicoViewModel } from "../../medicos/models/listar-medico.view-model"
import { TipoAtividadeEnum } from "./tipo-atividade.enum"

export type VisualizarAtividadeViewModel = {
    id: string
    descricao: string
    data: string
    horaInicio: string
    horaFim: string
    tipoAtividade: TipoAtividadeEnum
    medicos: Array<ListarMedicoViewModel>
}