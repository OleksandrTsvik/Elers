import { App, Button } from 'antd';
import { useTranslation } from 'react-i18next';

import { useDeleteTestQuestionMutation } from '../../api/test-questions.api';
import useDisplayError from '../../hooks/use-display-error';

interface Props {
  questionId: string | undefined;
  resetQuestionId: () => void;
}

export default function DeleteQuestionButton({
  questionId,
  resetQuestionId,
}: Props) {
  const { t } = useTranslation();

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteTestQuestion] = useDeleteTestQuestionMutation();

  const handleClick = async () => {
    await modal.confirm({
      title: t('material_test_edit_page.confirm_delete_question_title'),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        deleteTestQuestion({ id: questionId })
          .unwrap()
          .then(() => resetQuestionId())
          .catch((error) => displayError(error)),
    });
  };

  if (!questionId) {
    return null;
  }

  return (
    <Button className="right-btn" danger type="primary" onClick={handleClick}>
      {t('course_test.delete_question')}
    </Button>
  );
}
