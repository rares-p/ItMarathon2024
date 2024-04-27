import {Injectable} from "@nestjs/common";
import {InjectRepository} from "@nestjs/typeorm";
import {PreferenceEntity} from "./entities/preference.entity";
import {Repository} from "typeorm";
import {UUID} from "../Utils/Types";
import {UserService} from "../UserService/user.service";
import {SubjectService} from "../SubjectService/subject.service";


export type Preference = {
    subjectId: UUID
}

@Injectable()
export class PreferenceService {
    constructor(
        @InjectRepository(PreferenceEntity)
        private readonly preferenceRepository: Repository<PreferenceEntity>,
        private readonly userService: UserService,
        private readonly subjectService: SubjectService
    ) {}

    public async setPreferences(studentId: UUID, subjectId: UUID, slot: number) {
        if (slot < 1 || slot > 4) {
            return "Slot must be between 1 and 4";
        }

        try {
            const student = await this.userService.getStudent(studentId);
            if (student == undefined)
                return "Student does not exist!";

            if (!(await this.subjectService.subjectExists(subjectId)))
                return "Subject does not exist!";

        } catch(err) {
            return undefined;
        }
    }
}