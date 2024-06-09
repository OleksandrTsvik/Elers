import { Select, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import DeleteQuestionButton from './delete-question.button';
import TestQuestionBodyForm from './test-question.body-form';
import { TestQuestionType } from '../../models/test-question.interface';

interface Props {
  testId: string;
  questionId: string | undefined;
  questionType: TestQuestionType;
  onChangeQuestionType: (value: TestQuestionType) => void;
}

export default function TestQuestionForm({
  testId,
  questionId,
  questionType,
  onChangeQuestionType,
}: Props) {
  const { t } = useTranslation();

  return (
    <>
      <DeleteQuestionButton questionId={questionId} />

      <Typography.Paragraph className="pb-label-field m-0">
        {t('course_test.question_type_title')}
      </Typography.Paragraph>

      <Select
        className="w-100 mb-field"
        disabled={!!questionId}
        value={questionType}
        options={Object.values(TestQuestionType).map((type) => ({
          label: t([`course_test.question_type.${type}`, type]),
          value: type,
        }))}
        onChange={onChangeQuestionType}
      />

      <TestQuestionBodyForm
        testId={testId}
        questionId={questionId}
        questionType={questionType}
      />
    </>
  );
}
