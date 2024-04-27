import {Injectable} from "@nestjs/common";
import {InjectRepository} from "@nestjs/typeorm";
import {PreferenceEntity} from "./entities/preference.entity";
import {Repository} from "typeorm";
import {UUID} from "../Utils/Types";
import {UserService} from "../UserService/user.service";
import {SubjectService} from "../SubjectService/subject.service";
import {StudentEntity, Years} from "../UserService/entities/student.entity";
import {StableMatchingService} from "../StableMatchingService/stableMatching.service";


export type Preference = {
    subjectId: UUID
}

type YearAndSubject = { year: Years, packet: number }
export type StudentPreferences = {
    student: StudentEntity,
    preferences: Array<UUID>
}

@Injectable()
export class PreferenceService {
    constructor(
        @InjectRepository(PreferenceEntity)
        private readonly preferenceRepository: Repository<PreferenceEntity>,
        private readonly userService: UserService,
        private readonly subjectService: SubjectService,
        private readonly stableMatchingService: StableMatchingService,
    ) {}

    public async setPreferences(studentId: UUID, preferences: Array<Preference>) {
        if (preferences.length < 1 || preferences.length > 4) {
            return "All preferences must be filled";
        }

        try {
            const student = await this.userService.getUser(studentId);
            if (student == undefined)
                return "Student does not exist!";

            const preferenceEntities = [];
            let packetId = undefined;
            for (let i = 0; i < preferences.length; ++i){
                const subject = await this.subjectService.getSubject(preferences[i].subjectId)
                if (subject == undefined)
                    return "Subject does not exist!";
                if (packetId == undefined)
                    packetId = subject.packet;
                else if (packetId != subject.packet)
                    return "All subjects must belong to the same packet!";

                const preference = this.preferenceRepository.create({
                    studentId: studentId,
                    slot: i,
                    subjectId: preferences[i].subjectId
                })

                preferenceEntities.push(preference);
            }

            for (const preferenceEntity of preferenceEntities) {
                await this.preferenceRepository.save(preferenceEntity);
            }

            return true;
        } catch(err) {
            return undefined;
        }
    }

    public async createMatching() {
        const completeMap: Map<Years, Map<number, Array<StudentPreferences>>> =
            new Map<Years, Map<number, Array<StudentPreferences>>>();
        const allPreferenceEntities = await this.preferenceRepository.find();
        const studentsDetails = new Map<UUID, StudentEntity>;

        try {
            const checkedSubjects: Map<UUID, YearAndSubject> = new Map<UUID, YearAndSubject>();

            for (let i = 0; i < allPreferenceEntities.length; ++i) {
                const preference = allPreferenceEntities[i];

                let studentEntity = studentsDetails.get(preference.studentId);
                if (studentEntity == undefined) {
                    studentEntity = await this.userService.getStudent(preference.studentId);
                    studentsDetails.set(preference.studentId, studentEntity);
                }

                let subject: YearAndSubject;
                if (!checkedSubjects.has(preference.subjectId)) {
                    const subjectEntity = await this.subjectService.getSubject(preference.subjectId)

                    checkedSubjects.set(preference.subjectId,
                        {year: subjectEntity.year, packet: subjectEntity.packet});
                    subject = {year: subjectEntity.year, packet: subjectEntity.packet};
                } else {
                    subject = checkedSubjects.get(preference.subjectId);
                }

                const year = subject.year;

                let subjectsOfYear = completeMap.get(year);
                if (subjectsOfYear == undefined) {
                    const packetToStudentPreferencesMap =
                        new Map<number, Array<StudentPreferences>>();
                    for (let packet = 0; packet < 4; ++packet) {
                        packetToStudentPreferencesMap.set(packet, [])
                    }
                }

                let packetPreferences = completeMap.get(year).get(subject.packet - 1);
                let studentPreferences = packetPreferences.find(studentPreference => studentPreference.student.id == preference.studentId);
                if (studentPreferences == undefined) {
                    studentPreferences = {
                        student: studentsDetails.get(preference.studentId),
                        preferences: ["", "", "", ""] as any
                    }
                    packetPreferences.push();
                }
                studentPreferences[preference.slot] = preference.subjectId;
            }
        } catch (err) {
            return undefined;
        }

        try {
            const years = [];

            for (let year: Years = 1; year < 6; ++year) {
                years.push([], [], [], [], []);
                const yearBucket = completeMap.get(year);
                const packetMatching = [];

                if (yearBucket == undefined || yearBucket.size == 0)
                    continue;

                for (let packet = 0; packet < 4; ++packet){
                    const packetBucket = yearBucket.get(packet);

                    years[packet] = await this.stableMatchingService.stableMatch(packetBucket);
                }

            }
        } catch (err) {
            return undefined;
        }
    }
}