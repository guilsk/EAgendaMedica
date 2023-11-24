import { TipoAtividadeEnum } from "./tipo-atividade.enum"

export type FormsAtividadeViewModel = {
    descricao: string
    data: string
    horaInicio: string
    horaFim: string
    tipoAtividade: TipoAtividadeEnum
    medicosId:Array<string>
}