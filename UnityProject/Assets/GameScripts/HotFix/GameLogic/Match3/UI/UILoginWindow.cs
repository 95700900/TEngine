using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UILoginWindow : UIWindow
    {
        #region 脚本工具生成的代码
        private TextMeshProUGUI m_tmp_title;
        private Button m_btn_start;
        private TextMeshProUGUI m_tmp_start;
        private Button m_btn_cencel;
        private TextMeshProUGUI m_tmp_cecel;
        protected override void ScriptGenerator()
        {
            m_tmp_title = FindChildComponent<TextMeshProUGUI>("m_tmp_title");
            m_btn_start = FindChildComponent<Button>("m_btn_start");
            m_tmp_start = FindChildComponent<TextMeshProUGUI>("m_btn_start/m_tmp_start");
            m_btn_cencel = FindChildComponent<Button>("m_btn_cencel");
            m_tmp_cecel = FindChildComponent<TextMeshProUGUI>("m_btn_cencel/m_tmp_cecel");
            m_btn_start.onClick.AddListener(UniTask.UnityAction(OnClick_startBtn));
            m_btn_cencel.onClick.AddListener(UniTask.UnityAction(OnClick_cencelBtn));
        }
        #endregion

        #region 事件
        private async UniTaskVoid OnClick_startBtn()
        {
            await UniTask.Yield();
        }
        private async UniTaskVoid OnClick_cencelBtn()
        {
            await UniTask.Yield();
        }
        #endregion

    }
}