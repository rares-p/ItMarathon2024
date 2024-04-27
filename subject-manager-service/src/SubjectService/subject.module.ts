import {Module} from "@nestjs/common";
import {TypeOrmModule} from "@nestjs/typeorm";
import {SubjectEntity} from "./entities/subject.entity";
import {SubjectController} from "./subject.controller";
import {SubjectService} from "./subject.service";

@Module({
    imports: [TypeOrmModule.forFeature([SubjectEntity])],
    controllers: [SubjectController],
    exports: [SubjectService],
    providers: [SubjectService]
})
export class SubjectModule{}