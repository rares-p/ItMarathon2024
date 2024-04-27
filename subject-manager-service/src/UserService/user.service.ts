import {Injectable} from "@nestjs/common";
import {handleRetry, InjectRepository} from "@nestjs/typeorm";
import {Repository} from 'typeorm';
import {UserEntity, UserRole} from "./entities/user.entity";
import {StudentEntity, Years} from "./entities/student.entity";
import {UUID} from "../Utils/Types";
import {IdentifierService} from "../IdentifierService/identifier.service";

@Injectable()
export class UserService {
    constructor(
        @InjectRepository(UserEntity)
        private readonly userRepository: Repository<UserEntity>,
        @InjectRepository(StudentEntity)
        private readonly studentRepository: Repository<StudentEntity>,
        private readonly identifierService: IdentifierService
    ) {
    }

    async create(identifier: string, username: string, password: string) {
        try {
            const completeIdentifier = await this.identifierService.getOne(identifier);
            if (completeIdentifier.isUsed == true)
                return "Identifier already used!";

            const user = this.userRepository.create({
                username: username,
                password: password,
                identifier: identifier,
                role: completeIdentifier.role
            });
            await this.userRepository.save(user);

            if (completeIdentifier.role == UserRole.STUDENT) {
                const student = await this.studentRepository.findOneBy({
                    id: completeIdentifier.studentId
                });
                student.userId = user.id;
                await this.studentRepository.save(student);
            }
            await this.identifierService.use(completeIdentifier.value);

            return {
                id: user.id,
            }
        } catch (err) {
            return undefined;
        }
    }

    async createStudent(name: string, credits: number, grade: number, year: Years) {
        try {
            const student = this.studentRepository.create({
                name: name,
                credits: credits,
                grade: grade,
                year: year
            });

            await this.studentRepository.save(student);

            return student.id;
        } catch (err) {
            return undefined;
        }
    }

    async login(username: string, password: string) {
        try {
            const user = await this.userRepository.findOne({
                where: {
                    username: username,
                    password: password
                }
            })

            return {
                id: user.id,
                identifier: user.identifier,
                role: user.role
            };
        } catch (err) {
            return undefined;
        }
    }

    async getRole(userId: UUID): Promise<UserRole | undefined> {
        try {
            const user = await this.userRepository.findOne({
                where: {
                    id: userId
                }
            });

            return user.role;
        } catch (err) {
            return undefined;
        }
    }

    async getStudent(studentId: UUID) {
        try {
            return await this.userRepository.findOneBy({
                id: studentId,
                role: UserRole.STUDENT
            });
        } catch (err) {
            return undefined;
        }
    }

    async getAll() {
        try {
            return await this.userRepository.find();
        } catch (err) {
            return undefined;
        }
    }
}