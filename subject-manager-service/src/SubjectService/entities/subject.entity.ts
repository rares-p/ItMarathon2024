import {BeforeInsert, Column, Entity, PrimaryGeneratedColumn} from "typeorm";
import {Years} from "../../UserService/entities/student.entity";
import {UUID} from "../../Utils/Types";

@Entity({
    name: 'subjects',
})
export class SubjectEntity {
    @PrimaryGeneratedColumn("uuid")
    id: UUID;
    @BeforeInsert() genarate(){ this.id= crypto.randomUUID()}

    @Column()
    name: string;

    @Column({
        type: "enum",
        enum: Years,
    })
    year: Years;

    @Column()
    description: string;

    @Column()
    packet: number;
}
