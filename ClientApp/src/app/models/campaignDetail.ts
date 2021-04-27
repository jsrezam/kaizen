import { Customer } from "./customer";

export interface CampaignDetail {
    id?: number,
    customer?: Customer,
    callsNumber?: number,
    callDuration?: number,
    status?: string
}