import {Injectable} from "@nestjs/common";
import {IStableMatchingService} from "./IStableMatching.service";
import {UUID} from "../Utils/Types";
import {StudentEntity} from "../UserService/entities/student.entity";
import {StudentPreferences} from "../PreferenceService/preference.service";



@Injectable()
export class StableMatchingService implements IStableMatchingService{

    constructor() {}

    private sortSubjectPreferences() {}

    public async stableMatch(preferences: Array<StudentPreferences>):
        Promise<Array<{studentId: UUID, to: UUID}>>{

        const subjects: Set<UUID> = new Set<UUID>();
        const students: Map<UUID, StudentEntity> = new Map<UUID, StudentEntity>();

        for (const preference of preferences) {
            students.set(preference.student.id, undefined);

            for (const subject of preference.preferences)
                subjects.add(subject);
        }

        const subjectPreferences: Map<UUID, Array<UUID>> = new Map<UUID, Array<UUID>>();
        for (const subject of subjects) {
            subjectPreferences.set(subject, Array.from(students.keys()));


        }

        return {} as any;
    }
}