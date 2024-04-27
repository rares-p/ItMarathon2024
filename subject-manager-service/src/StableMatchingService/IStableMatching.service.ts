import {UUID} from "../Utils/Types";

export interface IStableMatchingService {
    stableMatch(preferences: Array<{studentId: UUID, preferences: Array<UUID>}>):
        Promise<Array<{studentId: UUID, to: UUID}>>
}