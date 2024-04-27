import {Module} from "@nestjs/common";
import {TypeOrmModule} from "@nestjs/typeorm";
import {PreferenceEntity} from "./entities/preference.entity";
import {SubjectEntity} from "../SubjectService/entities/subject.entity";
import {StudentEntity} from "../UserService/entities/student.entity";
import {UserEntity} from "../UserService/entities/user.entity";
import {PreferenceController} from "./preference.controller";
import {PreferenceService} from "./preference.service";
import {SubjectService} from "../SubjectService/subject.service";
import {UserService} from "../UserService/user.service";
import {IdentifierService} from "../IdentifierService/identifier.service";
import {IdentifierEntity} from "../IdentifierService/entities/indentifier.entity";
import {StableMatchingService} from "../StableMatchingService/stableMatching.service";

@Module({
    imports: [TypeOrmModule.forFeature([PreferenceEntity, SubjectEntity, StudentEntity, UserEntity, IdentifierEntity])],
    controllers: [PreferenceController],
    exports: [PreferenceService],
    providers: [PreferenceService, SubjectService, UserService, IdentifierService, StableMatchingService]
})
export class PreferenceModule{}
