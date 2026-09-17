using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) {
    // Tag가 item일 때
	if (collision.gameObject.tag == "Player") {
        Player1.score += 15;
		// Destroy Item
		Destroy(gameObject, 0f);
	}
}
}
