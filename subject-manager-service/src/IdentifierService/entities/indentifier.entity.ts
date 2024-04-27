import {BeforeInsert, Column, Entity, JoinColumn, OneToOne, PrimaryGeneratedColumn} from "typeorm";
import {UserEntity, UserRole} from "../../UserService/entities/user.entity";
import {StudentEntity} from "../../UserService/entities/student.entity";
import {UUID} from "../../Utils/Types";

@Entity({
    name: 'identifiers',
})
export class IdentifierEntity {
    @PrimaryGeneratedColumn("uuid")
    value: string;
    @BeforeInsert() genarate(){
        let identifier = "";
        for (let i = 0; i < 32; ++i) {
            identifier += Math.floor(Math.random() * 10).toString()
        }
        this.value = identifier;
        this.isUsed = false;
    }

    @Column()
    isUsed: boolean

    @Column({
        type: "enum",
        enum: UserRole,
        default: UserRole.STUDENT
    })
    role: UserRole;

    @Column({
        nullable: true
    })
    @OneToOne(() => StudentEntity)
    @JoinColumn({name: "studentId", referencedColumnName: "id"})
    studentId: UUID
}