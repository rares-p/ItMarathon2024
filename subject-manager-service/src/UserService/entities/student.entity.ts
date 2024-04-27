import {BeforeInsert, Column, Entity, Index, JoinColumn, OneToOne, PrimaryGeneratedColumn} from "typeorm";
import {UserEntity} from "./user.entity";

export enum Years {
    FIRST = 1,
    SECOND = 2,
    THIRD = 3,
    FOURTH = 4,
    FIFTH = 5
}

@Entity({
    name: 'students',
})
export class StudentEntity {
    @PrimaryGeneratedColumn("uuid")
    id: `${string}-${string}-${string}-${string}-${string}`;
    @BeforeInsert() genarate(){ this.id= crypto.randomUUID()}

    @OneToOne(() => UserEntity)
    @JoinColumn({name: 'id'})
    userId: string;

    @Column()
    name: string;

    @Column()
    credits: number;

    @Column()
    grade: number;

    @Column({
        type: "enum",
        enum: Years,
    })
    year: number;
}