import {Injectable} from "@nestjs/common";
import {InjectRepository} from "@nestjs/typeorm";
import {SubjectEntity} from "./entities/subject.entity";
import {Years} from "../UserService/entities/student.entity";
import {Repository} from "typeorm";
import {UUID} from "../Utils/Types";


@Injectable()
export class SubjectService {

    constructor(
        @InjectRepository(SubjectEntity)
        private readonly subjectRepository: Repository<SubjectEntity>
    ) {}

    public async create(name: string, year: Years, description: string,
                        packet: number) {
        try {
            const subject = this.subjectRepository.create({
                name: name,
                year: year,
                description: description,
                packet: packet
            })

            await this.subjectRepository.save(subject);
        } catch (err) {
            return undefined;
        }
    }

    public async getAll() {
        try {
            return await this.subjectRepository.find();
        } catch (err) {
            return undefined;
        }
    }

    public async getSubject(subjectId: UUID) {
        try {
            return await this.subjectRepository.findOneBy({
                id: subjectId
            });
        } catch (err) {
            return undefined;
        }
    }

}