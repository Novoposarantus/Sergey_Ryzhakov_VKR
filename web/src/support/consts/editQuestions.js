export const emptyQuestion = {
    text: "",
    questionTypeId: null,
    answers: [{...emptyAnswer},{...emptyAnswer},{...emptyAnswer}]
}

export const emptyAnswer = {
    text: "",
    isRight: false
}

export const minAnswersCount = 3;
