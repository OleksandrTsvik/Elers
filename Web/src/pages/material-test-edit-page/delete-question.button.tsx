import { Button } from 'antd';
import { useTranslation } from 'react-i18next';

interface Props {
  questionId: string | undefined;
}

export default function DeleteQuestionButton({ questionId }: Props) {
  const { t } = useTranslation();

  if (!questionId) {
    return null;
  }

  return (
    <Button className="right-btn" danger type="primary">
      {t('course_test.delete_question')}
    </Button>
  );
}
