import { TipoAtividadeEnum } from "./tipo-atividade.enum"

export type ListarAtividadeViewModel = {
    id: string
    descricao: string
    data: string
    horaInicio: string
    horaFim: string
    tipoAtividade: TipoAtividadeEnum
}