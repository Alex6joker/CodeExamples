package com.example.GuessTheNumber;

public class Questions {

    private int Target; // Идентификатор ресурса с текстом вопроса/цели
    private boolean TrueFalse; // Верный/неверный ответ на вопрос

    public int getTarget() {
        return Target;
    }

    public boolean isTrueFalse() {
        return TrueFalse;
    }

    public void setTarget(int mTarget) {
        this.Target = mTarget;
    }

    public void setTrueFalse(boolean mTrueFalse) {
        this.TrueFalse = mTrueFalse;
    }

    public Questions(int nTarget, boolean nTrueFalse){
        Target = nTarget;
        TrueFalse = nTrueFalse;
    }
}
