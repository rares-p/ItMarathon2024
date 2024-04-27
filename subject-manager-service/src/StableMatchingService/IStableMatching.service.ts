import {UUID} from "../Utils/Types";
import {StudentPreferences} from "../PreferenceService/preference.service";

export interface IStableMatchingService {
    stableMatch(preferences: Array<StudentPreferences>):
        Promise<Array<{studentId: UUID, to: UUID}>>
}