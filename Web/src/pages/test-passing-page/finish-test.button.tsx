import { App, Button } from 'antd';
import { useTranslation } from 'react-i18next';
import { useNavigate, useParams } from 'react-router-dom';

import { useFinishTestMutation } from '../../api/tests.api';
import { ErrorAlert } from '../../common/error';

interface Props {
  testSessionId: string;
  testId: string;
}

export default function FinishTestButton({ testSessionId, testId }: Props) {
  const { courseId } = useParams();
  const navigate = useNavigate();

  const { t } = useTranslation();
  const { modal } = App.useApp();

  const [finishTest, { error }] = useFinishTestMutation();

  const handleClick = async () => {
    await modal.confirm({
      title: t('course_test.finish_test'),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        finishTest({ testSessionId })
          .unwrap()
          .then(() => navigate(`/courses/${courseId}/test/${testId}`)),
    });
  };

  return (
    <>
      <Button
        className="right-btn mt-field"
        danger
        type="primary"
        onClick={handleClick}
      >
        {t('course_test.finish_test')}
      </Button>

      <ErrorAlert className="mt-field" error={error} />
    </>
  );
}
