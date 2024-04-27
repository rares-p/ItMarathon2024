import {Injectable} from "@nestjs/common";
import {IStableMatchingService} from "./IStableMatching.service";
import {UUID} from "../Utils/Types";



@Injectable()
export class StableMatchingService implements IStableMatchingService{

    constructor() {}


    public async stableMatch(preferences: Array<{studentId: UUID, preferences: Array<UUID>}>):
        Promise<Array<{studentId: UUID, to: UUID}>>{

        return {} as any;
    }
}